using CryptoPro.ExchangeClients.Infrastructure.Common;

namespace CryptoPro.ExchangeClients.Infrastructure.Clients.Rest.CoinGecko;

public sealed class CoinGeckoEndpoint : BaseType
{
    private CoinGeckoEndpoint(string value) : base(value) { }
    
    public static readonly CoinGeckoEndpoint CoinMarkets = new("/api/v3/coins/markets");
}