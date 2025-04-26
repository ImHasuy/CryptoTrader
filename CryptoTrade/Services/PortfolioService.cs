using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class PortfolioService : IPortfolioService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public PortfolioService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<PortfolioDto>> GetPortfolioAsync(string id)
        {
            var Wallet = await _context.Wallets.Include(w => w.OwnedCryptos).ThenInclude(t => t.Crypto).FirstOrDefaultAsync(w => w.UserId.ToString() == id) ?? throw new Exception($"Wallet with user id {id} not found");
            var res = _mapper.Map<List<PortfolioDto>>(Wallet.OwnedCryptos);
            return res; 
        }
    }

}
