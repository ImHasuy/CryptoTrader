using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class TransactionLog
    {
        [Required,Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();

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
