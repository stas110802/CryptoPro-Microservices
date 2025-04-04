using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

public class CoinFullDataById
{
    [JsonProperty("description")] 
    public Dictionary<string, string> Description { get; set; }

    [JsonProperty("market_cap_rank")] 
    public long MarketCapRank { get; set; } = 0;

    [JsonProperty("market_data")] 
    public CoinByIdMarketData MarketData { get; set; }
}