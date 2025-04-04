namespace CryptoPro.WpfApp.MVVM.Models;

public class MarketChart
{
    public decimal[][] Prices { get; set; }
    public decimal[][] MarketCaps { get; set; }
    public decimal[][] TotalVolumes { get; set; }
}