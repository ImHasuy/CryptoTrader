using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class TransactionLog
    {
        [Required,Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CryptoId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public bool IsBuy { get; set; } //true if buy, false if sell
        public DateTime Date { get; set; }
    }
}
