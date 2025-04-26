using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class CryptoTradeRepository :ICryptoTradeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CryptoTradeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> BuyCryptoAsync(CryptoTradeDTOtoFunc CreateTradeDTO)
        {
            var manager = GetService();
            return await manager.BuyCryptoAsync(CreateTradeDTO);
        }

        public async Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO)
        {
            var manager = GetService();
            return await manager.SellCryptoAsync(createTradeDTO);
        }

        private ICryptoTradeService GetService()
        {
            return new CryptoTradeService(_context, _mapper);
        }
    }
}
