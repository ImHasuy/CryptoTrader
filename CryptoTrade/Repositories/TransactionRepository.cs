using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class TransactionRepository :ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionLogGetDetailedDto> GetTransactionDetailsAsync(string id)
        {
            var manager = GetService();
            return await manager.GetTransactionDetailsAsync(id);
        }

        public async Task<List<TransactionLogGetDto>> ListTransactionsAsync(string id)
        {
            var manager = GetService();
            return await manager.ListTransactionsAsync(id);
        }

        private ITransactionLogService GetService()
        {
            return new TransactionLogService(_context, _mapper);
        }
    }
}
