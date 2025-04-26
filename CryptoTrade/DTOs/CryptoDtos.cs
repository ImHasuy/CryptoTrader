namespace CryptoTrade.DTOs
{
    public class CryptoDTO
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class CryptoUpdateDTO
    {
        public string id { get; set; }
        public double Value { get; set; }
    }
}
