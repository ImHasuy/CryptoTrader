using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    //Works as a Connection table between Crypto and Wallet, but with extended params
    public class CryptoWallet
    {
        [Required,Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Wallet")]
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = new Wallet();

        [ForeignKey("Crypto")]
        public Guid CryptoId { get; set; }
        public Crypto Crypto { get; set; } = new Crypto();
        [Required]
        public double Amount { get; set; }
        [Required]
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
