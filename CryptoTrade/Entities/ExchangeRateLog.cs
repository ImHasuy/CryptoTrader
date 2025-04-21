using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    public class ExchangeRateLog
    {
        [Required, Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Crypto")]
        public Guid CryptoId { get; set; }
        public Crypto Crypto { get; set; } = new Crypto();
        [Required]
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
