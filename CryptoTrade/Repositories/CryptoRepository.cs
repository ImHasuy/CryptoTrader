using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class CryptoRepository :ICryptoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CryptoRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ExchangeRateLog>> GetCryptoLogsByIdAsync(string cryptoid)
        {
            var manager = GetService();
            return manager.GetCryptoLogsByIdAsync(cryptoid);
        }

        public async Task<string> UpdateCryptoByIdAsync(CryptoUpdateDTO cryptoUpdateDTO)
        {
            var manager = GetService();
            return await manager.UpdateCryptoByIdAsync(cryptoUpdateDTO);
        }

        private ICryptoService GetService()
        {
            return new CryptoService(_context, _mapper);
        }

    }
}
