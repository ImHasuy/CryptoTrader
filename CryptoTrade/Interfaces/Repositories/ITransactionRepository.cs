using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<TransactionLogGetDto>> ListTransactionsAsync(string id);
        Task<TransactionLogGetDetailedDto> GetTransactionDetailsAsync(string id);
    }
}
