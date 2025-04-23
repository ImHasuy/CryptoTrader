namespace CryptoTrade.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IUserServices UserService { get; }
        IWalletService WalletService { get; }
        ICryptoTradeService CryptoTradeService { get; }
    }
}
