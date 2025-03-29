using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

public class Sparkline
{
    [JsonProperty("price")]
    public List<decimal> Prices { get; set; }
}