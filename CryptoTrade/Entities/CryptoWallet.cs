using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class CryptoWallet
    {
        public int Id { get; set; }

        [ForeignKey("Wallet")]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; } = new Wallet();

        [ForeignKey("Crypto")]
        public int CryptoId { get; set; }
        public Crypto Crypto { get; set; } = new Crypto();

        public double Amount { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
