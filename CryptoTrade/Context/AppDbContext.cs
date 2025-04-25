using CryptoTrade.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<CryptoWallet> CryptoWallets { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<ExchangeRateLog> ExchangeRateLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //User entity configuration

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Wallet entity configuration

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.OwnedCryptos)
                .WithOne(cw => cw.Wallet)
                .HasForeignKey(f => f.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            //CryptoWallet configuration

            modelBuilder.Entity<CryptoWallet>()
                .HasOne(cw => cw.Crypto)
                .WithMany()
                .HasForeignKey(cw => cw.CryptoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CryptoWallet>()
                .HasOne(cw => cw.Wallet)
                .WithMany(cw => cw.OwnedCryptos)
                .HasForeignKey(cw => cw.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
