using CryptoTrade.DTOs;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDto>> GetPortfolioAsync(string id);
    }
}
