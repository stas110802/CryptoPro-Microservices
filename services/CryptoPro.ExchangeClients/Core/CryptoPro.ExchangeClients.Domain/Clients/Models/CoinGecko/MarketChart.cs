using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

public class MarketChart
{
    [JsonProperty("prices")]
    public decimal[][] Prices { get; set; } = [];

    [JsonProperty("market_caps")]
    public decimal[][] MarketCaps { get; set; } = [];

    [JsonProperty("total_volumes")]
    public decimal[][] TotalVolumes { get; set; } = [];
}