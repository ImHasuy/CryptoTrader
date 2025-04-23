using CryptoTrade.DTOs;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface ICryptoTradeService
    {
        Task<bool> BuyCryptoAsync(CryptoTradeDTOtoFunc CreateTradeDTO);
        Task<bool> SellCryptoAsync(CryptoTradeDTOtoFunc createTradeDTO);
        Task<bool> GetCryptoPortofolioAsync(int id); //later bool has to be changet to the proper dto
    }
}
