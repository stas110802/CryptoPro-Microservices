namespace CryptoPro.WpfApp.MVVM.Models;

public class CoinFullData
{
    public Dictionary<string, string> Description { get; set; }
    public long MarketCapRank { get; set; } = 0;
    public CoinMarketData MarketData { get; set; }
    
    public string EnglishDescription => Description["en"];
}