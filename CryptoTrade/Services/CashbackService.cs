using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public interface ICashbackService
    {
        Task<List<CashBackDto>> GetCashbacks();
        Task<string> ModifyCashback(List<CashBackDto> lista);
    }

    public class CashbackService : ICashbackService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CashbackService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CashBackDto>> GetCashbacks()
        {
            var cashbacks = await _context.Cashbacks.ToListAsync() ?? throw new Exception("No cashbacks found in the database.");
            return _mapper.Map<List<CashBackDto>>(cashbacks) ?? throw new Exception("Error while mapping");

        }

        public async Task<string> ModifyCashback(List<CashBackDto> lista)
        {
            var cashbackInUse = await _context.Cashbacks.ToListAsync();
            foreach (var cashback in cashbackInUse)
            {
                _context.Cashbacks.Remove(cashback);
            }

            foreach (var cashbackDto in lista)
            {
                var cashback = _mapper.Map<CashBack>(cashbackDto);
                await _context.Cashbacks.AddAsync(cashback);
            }
            await _context.SaveChangesAsync();
            return "Cashback rules modified successfully";
        }
    }
}
