using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface ICryptoTradeRepository
    {
        Task<bool> BuyCryptoAsync(CryptoTradeDTOtoFunc CreateTradeDTO);
        Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO);
        
    }
}
