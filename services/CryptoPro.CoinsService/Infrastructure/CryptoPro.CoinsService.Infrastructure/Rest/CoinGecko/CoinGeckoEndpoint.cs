using CryptoPro.CoinsService.Infrastructure.Abstractions;

namespace CryptoPro.CoinsService.Infrastructure.Rest.CoinGecko;

public sealed class CoinGeckoEndpoint : BaseType
{
    private CoinGeckoEndpoint(string value) : base(value) { }
    
    public static readonly CoinGeckoEndpoint CoinMarkets = new("/api/v3/coins/markets");

    public static CoinGeckoEndpoint CoinDataById(string id)
    {
        return new CoinGeckoEndpoint($"/api/v3/coins/{id}");
    }

    public static CoinGeckoEndpoint CoinHistoricalChartDataTimeRange(string id)
    {
        return new CoinGeckoEndpoint($"/api/v3/coins/{id}/market_chart/range");
    }
}