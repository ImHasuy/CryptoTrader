using CryptoTrade.Entities.EntityRelatedEnums;

namespace CryptoTrade.DTOs
{
    public class SavingsInputDTO
    {
        public Guid UserId { get; set; }
        public Guid CryptoId { get; set; }
        public double Amount { get; set; }
        public int LockTimeInDays { get; set; }
    }
}
