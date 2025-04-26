using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class WalletRepository :IWalletRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WalletRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> DeleteWalletAsync(string id)
        {
            var manager = GetService();
            return await manager.DeleteWalletAsync(id);
        }

        public async Task<WalletGetDto> GetWalletByUserIdAsync(string id)
        {
            var manager = GetService();
            return await manager.GetWalletByUserIdAsync(id);
        }

        public async Task<string> TopUpWalletBalanceAsync(string id, WalletTopUpDto walletTopUpDto)
        {
            var manager = GetService();
            return await manager.TopUpWalletBalanceAsync(id, walletTopUpDto);
        }

        private IWalletService GetService()
        {
            return new WalletService(_context, _mapper);
        }
    }
}
