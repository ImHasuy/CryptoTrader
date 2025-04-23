using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;

namespace CryptoTrade.Services
{
    public class WalletService : IWalletService
    {
        public Task<Wallet?> DeleteWalletAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet?> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet?> UpdateWalletAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
