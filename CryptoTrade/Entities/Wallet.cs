using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class Wallet
    {
        [Required, Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public double Balance { get; set; } = 1000.0;
        public List<CryptoWallet> OwnedCryptos { get; set; } = new List<CryptoWallet>();
    }  
}
