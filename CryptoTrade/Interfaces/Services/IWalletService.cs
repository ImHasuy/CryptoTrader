using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IWalletService
    {
        Task<WalletGetDto> GetWalletByUserIdAsync(string id);
        Task<string> TopUpWalletBalanceAsync(string id, WalletTopUpDto walletTopUpDto);
        Task<string> DeleteWalletAsync(string id);
    }
}
