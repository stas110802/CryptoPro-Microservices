using System.Windows.Media;
using Newtonsoft.Json;

namespace CryptoPro.WpfApp.MVVM.Models;

public class Coin
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string ImageUri { get; set; }
    public decimal CurrentPrice { get; set; }
    public long MarketCap { get; set; }
    public float TotalVolume { get; set; }
    public float HighestPrice24h { get; set; }
    public float LowestPrice24h { get; set; }
    public float PriceChangePercentage1h { get; set; }
    public float PriceChangePercentage24h { get; set; }
    public float PriceChangePercentage7d { get; set; }
    public float PriceChangePercentage14d { get; set; }
    public float PriceChangePercentage30d { get; set; }
    public float PriceChangePercentage1y { get; set; }
    public float CirculatingSupply { get; set; }
    public Sparkline? SparklineIn7d { get; set; }

    public Brush PriceChangePercentage24hColor => GetColorByValue(PriceChangePercentage24h);
    public Brush PriceChangePercentage7dColor => GetColorByValue(PriceChangePercentage7d);

    private Brush GetColorByValue(float value)
    {
        return value > 0 ? Brushes.Red : Brushes.Green;
    }
}