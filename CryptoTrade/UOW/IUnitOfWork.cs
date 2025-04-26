using AutoMapper;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;

namespace CryptoTrade.UOW
{
    public interface IUnitOfWork 
    {
        IUserRepository UserRepository { get; }
        IWalletRepository WalletRepository { get; }
        ICryptoTradeRepository  CryptoTradeRepository{ get; }
        ICryptoRepository  CryptoRepository{ get; }
        ICryptosRepository CryptosRepository { get; }
        IPortfolioRepository PortfolioRepository { get; }
        IProfitRepository  ProfitRepository{ get; }
        ITransactionRepository  TransactionRepository{ get; }
    }
}
