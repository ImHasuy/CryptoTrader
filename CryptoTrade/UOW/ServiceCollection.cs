using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.UOW
{
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<ICryptoTradeService, CryptoTradeService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IProfitService, ProfitService>();
            services.AddScoped<ITransactionLogService, TransactionLogService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IUnitOfWork, ProductionUnitOfWork>();
            services.AddHostedService<CryptoExchRateUpdateBGService>();

        }
    }
}
