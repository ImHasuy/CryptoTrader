using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface ICryptoTradeRepository
    {
        Task<ReturnTradeValue> BuyCryptoAsync(CryptoTradeDTOtoFunc CreateTradeDTO);
        Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO);
        
    }
}
