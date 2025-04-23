using CryptoTrade.Entities;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet?> GetUserByIdAsync(string id);
        Task<Wallet?> UpdateWalletAsync(string id);
        Task<Wallet?> DeleteWalletAsync(string id);
    }
}
