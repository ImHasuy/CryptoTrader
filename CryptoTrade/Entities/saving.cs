using CryptoTrade.Entities.EntityRelatedEnums;

namespace CryptoTrade.Entities
{
    public class Saving
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CryptoId { get; set; }
        public double Amount { get; set; }
        public double ExcpedtedValueAfterLock { get; set; }
        public  int LockTimeInDays { get; set; }
        public double DailyInterest { get; set; }
        public SavingStatus Status { get; set; } = SavingStatus.active;
    }
}
