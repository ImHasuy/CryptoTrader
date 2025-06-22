using CryptoTrade.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.DTOs
{
    public class CryptoTradeDTO
    {
        public string CryptoId { get; set; }
        public double Amount { get; set; }
    }

    public class CryptoTradeDTOtoFunc
    {
        public string UserGuid { get; set; }
        public string CryptoId { get; set; }
        public double Amount { get; set; }
    }
    
    public class ReturnTradeValue
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CryptoId { get; set; }
        public double Amount { get; set; }
        public double CashBackAmount { get; set; }
        public double TotalValue { get; set; }
        public DateTime Date { get; set; }
    }
}
