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
        private readonly IConfiguration _configuration;
        IUserServices userServices;

        public ProductionUnitOfWork(AppDbContext context, IMapper mapper, IConfiguration configuration )
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            userServices = new UserService(_context, _mapper,_configuration );
        }

        public IUserServices UserService => userServices;
    }
}
