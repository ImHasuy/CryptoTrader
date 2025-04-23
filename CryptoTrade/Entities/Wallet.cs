using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Entities
{
    public class Wallet
    {
        [Required, Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public double Balance { get; set; } = 1000.0;
        public List<CryptoWallet> OwnedCryptos { get; set; } = new List<CryptoWallet>();
    }
}
