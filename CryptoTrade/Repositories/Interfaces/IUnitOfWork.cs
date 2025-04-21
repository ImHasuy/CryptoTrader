namespace CryptoTrade.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IUserServices UserService { get; }
    }
}
