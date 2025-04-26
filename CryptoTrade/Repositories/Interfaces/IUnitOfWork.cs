using AutoMapper;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IUserServices UserService { get; }
        IWalletService WalletService { get; }
        ICryptoTradeService CryptoTradeService { get; }
        ICryptoService CryptoService { get; }
        IPortfolioService PortfolioService { get; }
        IProfitService ProfitService { get; }
        ITransactionLogService TransactionLogService { get; }
    }
}
