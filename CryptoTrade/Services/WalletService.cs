using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WalletService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;

        }


        public async Task<string> DeleteWalletAsync(string id)
        {
            var wallet = await _context.Wallets.Include(w=>w.User).FirstOrDefaultAsync(w => w.UserId.ToString() == id) ?? throw new Exception($"Wallet with user id {id} not found");
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return "Wallet successfully deleted";
        }

        public async Task<WalletGetDto> GetWalletByUserIdAsync(string id)
        {
           var Wallet = await _context.Wallets.Include(w=>w.OwnedCryptos).ThenInclude(t=>t.Crypto).FirstOrDefaultAsync(w => w.UserId.ToString() == id) ?? throw new Exception($"Wallet with user id {id} not found");
           var res = _mapper.Map<WalletGetDto>(Wallet);
           res.OwnedCryptos = _mapper.Map<List<CryptoWalletGetDto>>(Wallet.OwnedCryptos);

           return res;
        }

        public async Task<string> TopUpWalletBalanceAsync(string id, WalletTopUpDto walletTopUpDto)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId.ToString() == id) ?? throw new Exception($"Wallet with user id {id} not found");
            wallet.Balance += walletTopUpDto.BalanceToTopUp;
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
            return "The top-up was succesfull";
        }
    }
}
