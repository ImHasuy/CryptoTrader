using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Entities
{
    public class Wallet
    {
        [Required, Key]
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public List<CryptoWallet> OwnedCryptos { get; set; } = new List<CryptoWallet>();
    }
}
