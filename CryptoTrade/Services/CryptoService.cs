using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CryptoService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<List<CryptoDTO>> GetCryptosAsync()
        {
            var cryptos = await _context.Cryptos.ToListAsync();
            return _mapper.Map<List<CryptoDTO>>(cryptos);
        }

        public async Task<Crypto> GetCryptoByIdAsync(string id)
        {
            return await _context.Cryptos.FirstOrDefaultAsync(c=>c.Id.ToString() == id) ?? throw new Exception($"Crypto with {id} not found") ;
        }

        public async Task<string> AddNewCryptoAsync(CryptoDTO cryptoCreateDTO)
        {
            var crypto = _mapper.Map<Crypto>(cryptoCreateDTO) ?? throw new Exception("Error while mapping the CryptoDto");
            await _context.Cryptos.AddAsync(crypto);
            await _context.SaveChangesAsync();

            return "Crypto Created Successfully";
        }
        public async Task<string> DeletCryptoByIdAsync(string id)
        {
            var crypto = await _context.Cryptos.FirstOrDefaultAsync(c => c.Id.ToString() == id) ?? throw new Exception($"Crypto with {id} not found");
            _context.Cryptos.Remove(crypto);
            await _context.SaveChangesAsync();

            return "Crypto Deleted Successfully";
        }

        
        public async Task<string> UpdateCryptoByIdAsync(CryptoUpdateDTO cryptoUpdateDTO)
        {
            var crypto = await _context.Cryptos.FirstOrDefaultAsync(c => c.Id.ToString() == cryptoUpdateDTO.id) ?? throw new Exception($"Crypto with {cryptoUpdateDTO.id} not found");
            crypto.Value = cryptoUpdateDTO.Value;
            _context.Cryptos.Update(crypto);
            await _context.SaveChangesAsync();
            return "Crypto Updated Successfully";
        }

        public async Task<List<ExchangeRateLog>> GetAllExchangeRateAsync()
        {
            return await _context.ExchangeRateLogs.ToListAsync();
        }

    }
}
