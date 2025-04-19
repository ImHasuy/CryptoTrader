using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Entities
{
    public class Crypto
    {
        [Required,Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}
