using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface ICryptoRepository
    {
        Task<List<ExchangeRateLog>> GetCryptoLogsByIdAsync(string cryptoid);
        Task<string> UpdateCryptoByIdAsync(CryptoUpdateDTO cryptoUpdateDTO);
    }
}
