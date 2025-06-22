using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public interface ISavingsService
    {
        Task<Saving> CreateSaving(SavingsInputDTO savingsInputDTO);
       // Task<string> ModifyCashback(List<CashBackDto> lista);
    }

    public class SavingsService : ISavingsService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SavingsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Saving> CreateSaving(SavingsInputDTO savingsInputDTO)
        {
            var user = await _context.Users.FindAsync(savingsInputDTO.UserId) ?? throw new Exception($"User with id {savingsInputDTO.UserId} not found");
            var crypto = await _context.Cryptos.FindAsync(savingsInputDTO.CryptoId) ?? throw new Exception($"Crypto with id {savingsInputDTO.CryptoId} not found");
            var UserSCryptoWallet = await _context.CryptoWallets.Include(x => x.Wallet).FirstOrDefaultAsync(l => l.Wallet.UserId == savingsInputDTO.UserId && l.CryptoId == savingsInputDTO.CryptoId) ?? throw new Exception($"User with id {savingsInputDTO.UserId} does not have a wallet with crypto {savingsInputDTO.CryptoId}");

            if (UserSCryptoWallet.Amount <= savingsInputDTO.Amount)
            {
                throw new Exception("Not enough crypto in wallet");
            }
            var intRate = await _context.InterestRates.FirstOrDefaultAsync(i => i.CryptoId == savingsInputDTO.CryptoId) ?? throw new Exception("Interest rate not found");

            var expected  = savingsInputDTO.Amount * (intRate.InterestRate / 100);
            var saving = new Saving
            {
                UserId = user.Id,
                CryptoId = crypto.Id,
                Amount = savingsInputDTO.Amount,
                ExcpedtedValueAfterLock = (crypto.Value * savingsInputDTO.Amount) * (expected*savingsInputDTO.LockTimeInDays),
                LockTimeInDays = savingsInputDTO.LockTimeInDays,
                DailyInterest = expected
            };

            UserSCryptoWallet.Amount -= savingsInputDTO.Amount;

            await _context.Savings.AddAsync(saving);
            await _context.SaveChangesAsync();
            return saving;

        }

        //public async Task<string> ModifyCashback(List<CashBackDto> lista)
        //{
        //    var cashbackInUse = await _context.Cashbacks.ToListAsync();
        //    foreach (var cashback in cashbackInUse)
        //    {
        //        _context.Cashbacks.Remove(cashback);
        //    }

        //    foreach (var cashbackDto in lista)
        //    {
        //        var cashback = _mapper.Map<CashBack>(cashbackDto);
        //        await _context.Cashbacks.AddAsync(cashback);
        //    }
        //    await _context.SaveChangesAsync();
        //    return "Cashback rules modified successfully";
        //}
    }
}
