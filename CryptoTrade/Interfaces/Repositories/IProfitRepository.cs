using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface IProfitRepository
    {
        Task<double> GetAllProfitAsync(string id);
        Task<List<ProfitDto>> GetDetailedProfitAsync(string id);
    }
}
