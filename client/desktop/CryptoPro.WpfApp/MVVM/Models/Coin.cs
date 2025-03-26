using CryptoPro.WpfApp.ValueObjects;

namespace CryptoPro.WpfApp.MVVM.Models;

public class Coin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
    public PriceChangePercentage PriceChangePercentage24H { get; set; }
    public PriceChangePercentage PriceChangePercentage7D { get; set; }
    public long MarketCap { get; set; }
    public long Volume24H { get; set; }
    public long CirculatingSupply { get; set; }
}