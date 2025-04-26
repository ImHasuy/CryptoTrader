using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class CryptosRepository : ICryptosRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CryptosRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddNewCryptoAsync(CryptoDTO cryptoCreateDTO)
        {
            var manager = GetService();
            return await manager.AddNewCryptoAsync(cryptoCreateDTO);
        }

        public async Task<string> DeletCryptoByIdAsync(string id)
        {
            var manager = GetService();
            return await manager.DeletCryptoByIdAsync(id);
        }

        public Task<Crypto> GetCryptoByIdAsync(string id)
        {
            var manager = GetService();
            return manager.GetCryptoByIdAsync(id);

        }

        public async Task<List<CryptoDTO>> GetCryptosAsync()
        {
            var manager = GetService();
            return await manager.GetCryptosAsync();
        }


        private ICryptoService GetService()
        {
            return new CryptoService(_context, _mapper);
        }

    }
}
