using CryptoTrade.DTOs;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IProfitService
    {
        Task<double> GetAllProfitAsync(string id);
        Task<List<ProfitDto>> GetDetailedProfitAsync(string id);
    }
}
