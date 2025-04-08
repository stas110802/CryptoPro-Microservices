using Newtonsoft.Json;

namespace CryptoPro.CoinsService.Domain.Models.CoinGecko;

public class Sparkline
{
    [JsonProperty("price")]
    public List<decimal> Prices { get; set; }
}