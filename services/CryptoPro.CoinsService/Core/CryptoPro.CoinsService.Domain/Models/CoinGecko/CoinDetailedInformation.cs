using Newtonsoft.Json;

namespace CryptoPro.CoinsService.Domain.Models.CoinGecko;

public class CoinDetailedInformation
{
    [JsonProperty("description")] 
    public Dictionary<string, string> Description { get; set; }

    [JsonProperty("market_cap_rank")] 
    public long MarketCapRank { get; set; } = 0;

    [JsonProperty("market_data")] 
    public CoinMarketData MarketData { get; set; }
}