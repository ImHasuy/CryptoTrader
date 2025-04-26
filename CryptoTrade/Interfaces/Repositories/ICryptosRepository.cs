using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface ICryptosRepository
    {
        Task<List<CryptoDTO>> GetCryptosAsync();
        Task<string> AddNewCryptoAsync(CryptoDTO cryptoCreateDTO);
        Task<string> DeletCryptoByIdAsync(string id);
        Task<Crypto> GetCryptoByIdAsync(string id);

    }
}
