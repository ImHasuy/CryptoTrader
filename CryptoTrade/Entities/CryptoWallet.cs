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
        public Wallet Wallet { get; set; }


        [ForeignKey("Crypto")]
        public Guid CryptoId { get; set; }
        public Crypto Crypto { get; set; } 

        [Required]
        public double Amount { get; set; } //It stores how much a crypto costed when i bougt
        [Required]
        public double Value { get; set; } //Stores the value of the crypto at the time of purchase
        public DateTime Date { get; set; } //Stores when i managed last time my cryptos
    }
}
