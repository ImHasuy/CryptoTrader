using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PortfolioRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PortfolioDto>> GetPortfolioAsync(string id)
        {
            var manager = GetService();
            return await manager.GetPortfolioAsync(id);
        }

        private IPortfolioService GetService()
        {
            return new PortfolioService(_context, _mapper);
        }
    }
}
