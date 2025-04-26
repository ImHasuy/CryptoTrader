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
        ICryptoService cryptoService;
        IPortfolioService portfolioService;
        IProfitService profitService;
        ITransactionLogService transactionLogService;


        public ProductionUnitOfWork(AppDbContext context, IMapper mapper, IConfiguration configuration )
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            userServices = new UserService(_context, _mapper,_configuration );
            walletService = new WalletService(_context, _mapper, _configuration); 
            cryptoTradeService = new CryptoTradeService(_context, _mapper, _configuration); 
            cryptoService = new CryptoService(_context, _mapper, _configuration);
            portfolioService = new PortfolioService(_context, _mapper, _configuration);
            profitService = new ProfitService(_context, _mapper, _configuration);
            transactionLogService = new TransactionLogService(_context, _mapper, _configuration);
        }

        public IUserServices UserService => userServices;
        public IWalletService WalletService => walletService;
        public ICryptoTradeService CryptoTradeService => cryptoTradeService;
        public ICryptoService CryptoService => cryptoService;
        public IPortfolioService PortfolioService => portfolioService;
        public IProfitService ProfitService => profitService;
        public ITransactionLogService TransactionLogService => transactionLogService;

    }
}
