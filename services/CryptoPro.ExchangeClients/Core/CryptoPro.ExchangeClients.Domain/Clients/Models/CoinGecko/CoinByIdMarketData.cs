using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

public class CoinByIdMarketData
{
    [JsonProperty("current_price")]
    public Dictionary<string, decimal> CurrentPrice { get; set; }
    
    [JsonProperty("ath")]
    public Dictionary<string, decimal> Ath { get; set; }

    [JsonProperty("ath_change_percentage")]
    public Dictionary<string, decimal> AthChangePercentage { get; set; }

    [JsonProperty("ath_date")] 
    public Dictionary<string, DateTimeOffset?> AthDate { get; set; }

    [JsonProperty("atl")]
    public Dictionary<string, decimal> Atl { get; set; }

    [JsonProperty("atl_change_percentage")]
    public Dictionary<string, decimal> AtlChangePercentage { get; set; }

    [JsonProperty("atl_date")] 
    public Dictionary<string, DateTimeOffset> AtlDate { get; set; }
    
    [JsonProperty("market_cap")] 
    public Dictionary<string, decimal> MarketCap { get; set; }
    
    [JsonProperty("total_volume")] 
    public Dictionary<string, decimal> TotalVolume { get; set; }
    
    [JsonProperty("high_24h")] 
    public Dictionary<string, decimal> High24H { get; set; }
    
    [JsonProperty("low_24h")] 
    public Dictionary<string, decimal> Low24hH{ get; set; }
    
    #region Price Change Percentages
    
    [JsonProperty("price_change_percentage_24h")] 
    public float PriceChangePercentage24H { get; set; }
    
    [JsonProperty("price_change_percentage_7d")] 
    public float PriceChangePercentage7D { get; set; }
    
    [JsonProperty("price_change_percentage_14d")] 
    public float PriceChangePercentage14D { get; set; }
        
    [JsonProperty("price_change_percentage_30d")] 
    public float PriceChangePercentage30D { get; set; }
    
    [JsonProperty("price_change_percentage_1y")] 
    public float PriceChangePercentage1Y { get; set; }
    
    #endregion
    
    #region Price Change Percentages in Currencies
    
    [JsonProperty("price_change_percentage_24h_in_currency")] 
    public Dictionary<string, decimal> PriceChangePercentage24HInCurrency { get; set; }
    
    [JsonProperty("price_change_percentage_7d_in_currency")] 
    public Dictionary<string, decimal> PriceChangePercentage7DInCurrency { get; set; }
    
    [JsonProperty("price_change_percentage_14d_in_currency")] 
    public Dictionary<string, decimal> PriceChangePercentage14DInCurrency { get; set; }
    
    [JsonProperty("price_change_percentage_30d_in_currency")] 
    public Dictionary<string, decimal> PriceChangePercentage30DInCurrency { get; set; }
    
    [JsonProperty("price_change_percentage_1y_in_currency")] 
    public Dictionary<string, decimal> PriceChangePercentage1YInCurrency { get; set; }
    
    #endregion
    
    [JsonProperty("market_cap_change_24h_in_currency")] 
    public Dictionary<string, decimal> MarketCapChange24HInCurrency{ get; set; }
    
    [JsonProperty("market_cap_change_percentage_24h_in_currency")] 
    public Dictionary<string, decimal> MarketCapChangePercentage24HInCurrency{ get; set; }
    
    [JsonProperty("last_updated")] 
    public DateTimeOffset? LastUpdated { get; set; }
}