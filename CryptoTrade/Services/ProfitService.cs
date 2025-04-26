using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class ProfitService : IProfitService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProfitService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<double> GetAllProfitAsync(string id)
        {
            var cryptos = await _context.CryptoWallets
                .Include(c => c.Wallet)
                .Include(c => c.Crypto)
                .Where(x => x.Wallet.UserId.ToString() == id)
                .ToListAsync();

            if (cryptos.Count == 0)
            {
                throw new Exception("The User dont have any Cryptos");
            }

            return cryptos.Sum(c => (c.Crypto.Value - c.Value) * c.Amount);
        }

        public async Task<List<ProfitDto>> GetDetailedProfitAsync(string id)
        {
            List<ProfitDto> profits = new List<ProfitDto>();
            var cryptos = await _context.CryptoWallets
                .Include(c => c.Wallet)
                .Include(c => c.Crypto)
                .Where(x => x.Wallet.UserId.ToString() == id)
                .ToListAsync();

            if (cryptos.Count == 0)
            {
                throw new Exception("The User dont have any Cryptos");
            }

            foreach (var c in cryptos)
            {
                profits.Add(new ProfitDto
                {
                    Name = c.Crypto.Name,
                    ProfitAmount = (c.Crypto.Value - c.Value) * c.Amount
                });
            }

            return profits;
        }
    }
}
