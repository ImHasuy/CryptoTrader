using CryptoTrade.DTOs;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface ICryptoTradeService
    {
        Task<ReturnTradeValue> BuyCryptoAsync(CryptoTradeDTOtoFunc CreateTradeDTO);
        Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO);
        
    }
}
