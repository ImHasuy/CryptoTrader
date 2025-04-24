using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class ExchangeRateLog
    {
        [Required, Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string CryptoId { get; set; }
        [Required]
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
