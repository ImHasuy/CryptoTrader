using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<PortfolioDto>> GetPortfolioAsync(string id);
    }
}
