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
        public int CryptoId { get; set; }
        public Crypto Crypto { get; set; } = new Crypto();

        public double Amount { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
