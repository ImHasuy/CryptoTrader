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
        public double Amount { get; set; } //It stores how much a crypto costed when i bougt
        public double Value { get; set; } //Stores the value of the crypto at the time of purchase
        public DateTime Date { get; set; } //Stores when i managed last time my cryptos
    }
    public class WalletTopUpDto
    {
        public double BalanceToTopUp { get; set; }
    }


}
