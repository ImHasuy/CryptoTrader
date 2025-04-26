using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using System.Security.Claims;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface ICryptoService
    {
        Task<List<CryptoDTO>> GetCryptosAsync();
        Task<Crypto> GetCryptoByIdAsync(string id);
        Task<string> AddNewCryptoAsync(CryptoDTO cryptoCreateDTO);
        Task<string> DeletCryptoByIdAsync(string id);
        Task<List<ExchangeRateLog>> GetAllExchangeRateAsync();
        Task<string> UpdateCryptoByIdAsync(CryptoUpdateDTO cryptoUpdateDTO);
        Task<List<ExchangeRateLog>> GetCryptoLogsByIdAsync(string cryptoid);

    }
}
