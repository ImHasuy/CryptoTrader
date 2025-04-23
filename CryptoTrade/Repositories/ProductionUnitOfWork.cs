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
        IWalletService walletService;
        ICryptoTradeService cryptoTradeService;

        public ProductionUnitOfWork(AppDbContext context, IMapper mapper, IConfiguration configuration )
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            userServices = new UserService(_context, _mapper,_configuration );
            walletService = new WalletService(); //Later _context, _mapper, _configuration
            cryptoTradeService = new CryptoTradeService(_context, _mapper, _configuration); // same here
        }

        public IUserServices UserService => userServices;
        public IWalletService WalletService => walletService;
        public ICryptoTradeService CryptoTradeService => cryptoTradeService;
    }
}
