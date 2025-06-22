namespace CryptoTrade.Entities
{
    public class InterestRates
    {
        public Guid Id { get; set; }
        public Guid CryptoId { get; set; }
        public double InterestRate { get; set; }
    }
}
