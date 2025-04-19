using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class ProductionUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        IUserServicecs userServicecs;

        public ProductionUnitOfWork(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            userServicecs = new UserService(_context, _mapper);
        }

        public IUserServicecs UserService => userServicecs;
    }
}
