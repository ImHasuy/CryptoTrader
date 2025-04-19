using CryptoTrade.Context;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class ProductionUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        IUserServicecs userServicecs;

        public ProductionUnitOfWork(AppDbContext context)
        {
            _context = context;
            userServicecs = new UserService(_context);
        }

        public IUserServicecs UserService => userServicecs;
    }
}
