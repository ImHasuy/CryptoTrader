using CryptoTrade.DTOs;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletGetDto> GetWalletByUserIdAsync(string id);
        Task<string> TopUpWalletBalanceAsync(string id, WalletTopUpDto walletTopUpDto);
        Task<string> DeleteWalletAsync(string id);
    }
}
