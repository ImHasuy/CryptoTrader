using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public TransactionLogService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<TransactionLogGetDetailedDto> GetTransactionDetailsAsync(string tranid)
        {
            var Tran_log = await _context.TransactionLogs.OrderBy(x=>x.Date).FirstOrDefaultAsync(x => x.Id.ToString() == tranid) ?? throw new Exception($"Transaction with {tranid} not found"); ;
            var return_val =_mapper.Map<TransactionLogGetDetailedDto>(Tran_log);
            return_val.CryptoName = await _context.Cryptos.Where(x => x.Id.ToString() == Tran_log.CryptoId).Select(x => x.Name).FirstOrDefaultAsync() ?? throw new Exception($"Crypto with {Tran_log.CryptoId} not found");
            return return_val;
        }

        public async Task<List<TransactionLogGetDto>> ListTransactionsAsync(string id)
        {
            var Tran_log = await _context.TransactionLogs.Where(x => x.UserId == id).ToListAsync() ?? throw new Exception($"Transactions related to {id} not found");
            var return_val = Tran_log.Select(_mapper.Map<TransactionLogGetDto>).ToList();
            return return_val;
        }
    }
}
