namespace CryptoTrade.DTOs
{
    public class TransactionLogGetDetailedDto
    {
        public Guid Id { get; set; }
        public string CryptoName { get; set; }
        public string CryptoId { get; set; }
        public double Amount { get; set; }
        public double Value { get; set; }
        public double TotalValue { get; set; }
        public bool IsBuy { get; set; } //true if buy, false if sell
        public DateTime Date { get; set; }
    }

    public class TransactionLogGetDto
    {
        public Guid Id { get; set; }
        public bool IsBuy { get; set; } //true if buy, false if sell
        public DateTime Date { get; set; }
    }
}
