using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Entities
{
    public class Crypto
    {
        [Required,Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public double Value { get; set; }
    }
}
