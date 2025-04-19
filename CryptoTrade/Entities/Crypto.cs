using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Entities
{
    public class Crypto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}
