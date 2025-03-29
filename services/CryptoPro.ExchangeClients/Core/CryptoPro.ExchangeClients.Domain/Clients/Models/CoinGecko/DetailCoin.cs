using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

public class DetailCoin
{
    [JsonProperty("symbol")]
    public string ShortName { get; set; }
    
    [JsonProperty("name")]
    public string FullName { get; set; }
    
    [JsonProperty("image")]
    public string ImageUri { get; set; }
    
    [JsonProperty("current_price")]
    public decimal CurrentPrice { get; set; }
    
    [JsonProperty("market_cap")]
    public long MarketCap { get; set; }
    
    [JsonProperty("total_volume")]
    public float TotalVolume { get; set; }
    
    [JsonProperty("high_24h")]
    public float HighestPrice24h { get; set; }
    
    [JsonProperty("low_24h")]
    public float LowestPrice24h { get; set; }
    
    [JsonProperty("price_change_percentage_1h_in_currency")]
    public float PriceChangePercentage1h { get; set; }
    
    [JsonProperty("price_change_percentage_24h")]
    public float PriceChangePercentage24h { get; set; }
    
    [JsonProperty("price_change_percentage_7d_in_currency")]
    public float PriceChangePercentage7d { get; set; }
    
    [JsonProperty("price_change_percentage_14d_in_currency")]
    public float PriceChangePercentage14d { get; set; }
    
    [JsonProperty("price_change_percentage_30d_in_currency")]
    public float PriceChangePercentage30d { get; set; }
    
    [JsonProperty("price_change_percentage_1y_in_currency")]
    public float PriceChangePercentage1y { get; set; }
    
    [JsonProperty("circulating_supply")]
    public float CirculatingSupply { get; set; }
    
    [JsonProperty("sparkline_in_7d")]
    public Sparkline? SparklineIn7d { get; set; }
}