namespace CryptoTrade.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public List<CryptoWallet> OwnedCryptos { get; set; } = new List<CryptoWallet>();
    }
}
