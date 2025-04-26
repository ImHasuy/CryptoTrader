using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.UOW
{
    public class ProductionUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public IUserRepository UserRepository { get; }
        public IWalletRepository WalletRepository { get; }
        public ICryptoTradeRepository CryptoTradeRepository { get; }
        public ICryptoRepository CryptoRepository { get; }
        public ICryptosRepository CryptosRepository { get; }
        public IPortfolioRepository PortfolioRepository { get; }
        public IProfitRepository ProfitRepository { get; }
        public ITransactionRepository TransactionRepository { get; }



        public ProductionUnitOfWork(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;


            UserRepository = new UserRepository(_context, _mapper,_configuration );
            WalletRepository = new WalletRepository(_context, _mapper);
            CryptoTradeRepository = new CryptoTradeRepository(_context, _mapper);
            CryptoRepository = new CryptoRepository(_context, _mapper);
            PortfolioRepository = new PortfolioRepository(_context, _mapper);
            ProfitRepository = new ProfitRepository(_context, _mapper);
            TransactionRepository = new TransactionRepository(_context, _mapper);
            CryptosRepository = new CryptosRepository(_context, _mapper);
        }

  

    }
}
