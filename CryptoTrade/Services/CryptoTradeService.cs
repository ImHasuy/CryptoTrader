using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class CryptoTradeService : ICryptoTradeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CryptoTradeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> BuyCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO)
        {
            var user = await _context.Users.Include(u=>u.Wallet).FirstOrDefaultAsync(u => u.Id.ToString() == createTradeDTO.UserGuid);
            var crypto = await _context.Cryptos.FirstOrDefaultAsync(c => c.Id.ToString() == createTradeDTO.CryptoId);
            if (user != null && crypto != null)
            {
                var value = crypto.Value * createTradeDTO.Amount;

                var existingwallet = await _context.CryptoWallets.FirstOrDefaultAsync(c => c.CryptoId == crypto.Id && c.WalletId == user.Wallet.Id);
                //Check if the user has a wallet with the selected crypto
                if (existingwallet == null)
                {
                    if (user.Wallet.Balance < value)
                    {
                        throw new Exception($"Not enough balance in the wallet");
                    }

                    var l_wallet = await _context.Wallets.FirstOrDefaultAsync(u => u.UserId.ToString() == user.Wallet.UserId.ToString()) ?? throw new InvalidOperationException("Wallet not found.");

                    var subjectCrypto = new CryptoWallet
                    {
                        CryptoId = crypto.Id,
                        WalletId = user.Wallet.Id,
                        Amount = createTradeDTO.Amount,
                        Value = crypto.Value,
                        Date = DateTime.Now
                    };
                    var Tradelog = new TransactionLog
                    {
                        UserId = user.Id.ToString(),
                        CryptoId = crypto.Id.ToString(),
                        Amount = createTradeDTO.Amount,
                        Value = crypto.Value,
                        IsBuy = true,
                        Date = DateTime.Now
                    };

                    l_wallet.Balance -= value;
                    _context.Wallets.Update(l_wallet);
                    await _context.CryptoWallets.AddAsync(subjectCrypto);
                    await _context.TransactionLogs.AddAsync(Tradelog);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var l_wallet = await _context.Wallets.FirstOrDefaultAsync(u => u.Id.ToString() == user.Wallet.Id.ToString()) ?? throw new InvalidOperationException("Wallet not found.");
                    existingwallet.Amount += createTradeDTO.Amount;
                    existingwallet.Value = existingwallet.Value + value;
                    existingwallet.Date = DateTime.Now;

                    var Tradelog = new TransactionLog
                    {
                        UserId = user.Id.ToString(),
                        CryptoId = crypto.Id.ToString(),
                        Amount = createTradeDTO.Amount,
                        Value = value,
                        IsBuy = true,
                        Date = DateTime.Now
                    };

                    l_wallet.Balance -= value;
                    _context.Wallets.Update(l_wallet);
                    _context.CryptoWallets.Update(existingwallet);
                    await _context.TransactionLogs.AddAsync(Tradelog);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                throw new Exception($"User or Crypto with these ids not found");
            }
        }

        public async Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc sellTradeDTO)
        {
            var user = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id.ToString() == sellTradeDTO.UserGuid);
            var crypto = await _context.Cryptos.FirstOrDefaultAsync(c => c.Id.ToString() == sellTradeDTO.CryptoId);
            if (user != null && crypto != null)
            {
                var existingwallet = await _context.CryptoWallets
                    .FirstOrDefaultAsync(c => c.CryptoId == crypto.Id && c.WalletId == user.Wallet.Id);
                if (existingwallet == null || existingwallet.Amount < sellTradeDTO.Amount)
                {
                    throw new Exception($"User does not own the requied ammount of crypto / any of the selected crypto");
                }
                var l_wallet = await _context.Wallets.FirstOrDefaultAsync(u => u.Id.ToString() == user.Wallet.Id.ToString()) 
                    ?? throw new InvalidOperationException("Wallet not found.");

                var valuetoSell = crypto.Value * sellTradeDTO.Amount;//The User will get this amount of money
                existingwallet.Amount -= sellTradeDTO.Amount; //Decrease the amount of crypto in the Cryptowallet
                existingwallet.Date = DateTime.Now; //Update the date of the last transaction

                var Tradelog = new TransactionLog
                {
                    UserId = user.Id.ToString(),
                    CryptoId = crypto.Id.ToString(),
                    Amount = sellTradeDTO.Amount,
                    Value = crypto.Value,
                    IsBuy = false,
                    Date = DateTime.Now
                }; 

                l_wallet.Balance += valuetoSell; //Increase the balance with the value of the crypto sold

                _context.Wallets.Update(l_wallet);
                _context.CryptoWallets.Update(existingwallet);
                await _context.TransactionLogs.AddAsync(Tradelog);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception($"User or Crypto with these ids not found");
            }
        }

    }
}
