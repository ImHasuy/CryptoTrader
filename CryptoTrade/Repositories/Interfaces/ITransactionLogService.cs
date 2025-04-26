using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface ITransactionLogService
    {
        Task<List<TransactionLogGetDto>> ListTransactionsAsync(string id);
        Task<TransactionLogGetDetailedDto> GetTransactionDetailsAsync(string id);
    }
}
