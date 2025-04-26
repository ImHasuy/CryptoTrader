using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class ProfitRepository :IProfitRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfitRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<double> GetAllProfitAsync(string id)
        {
            var manager = GetService();
            return await manager.GetAllProfitAsync(id);
        }

        public async Task<List<ProfitDto>> GetDetailedProfitAsync(string id)
        {
            var manager = GetService();
            return await manager.GetDetailedProfitAsync(id);
        }

        private IProfitService GetService()
        {
            return new ProfitService(_context, _mapper);
        }
    }
}
