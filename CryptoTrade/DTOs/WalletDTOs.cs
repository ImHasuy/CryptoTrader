using CryptoTrade.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.DTOs
{
    public class WalletGetDto
    {
        public double Balance { get; set; }
        public List<CryptoWalletGetDto> OwnedCryptos { get; set; }
    }

    public class CryptoWalletGetDto
    {
        public string CryptoName { get; set; }
        public double Amount { get; set; } 
        public double Value { get; set; }
        public DateTime Date { get; set; } 
    }
    public class WalletTopUpDto
    {
        public double BalanceToTopUp { get; set; }
    }


}
