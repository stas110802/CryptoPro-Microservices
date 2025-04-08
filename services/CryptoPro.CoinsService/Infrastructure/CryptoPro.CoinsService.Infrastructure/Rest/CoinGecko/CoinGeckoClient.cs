using CryptoPro.CoinsService.Domain.Interfaces;
using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using CryptoPro.CoinsService.Infrastructure.Extensions;
using CryptoPro.CoinsService.Infrastructure.Options;
using RestSharp;
using Microsoft.Extensions.Options;

namespace CryptoPro.CoinsService.Infrastructure.Rest.CoinGecko;

public sealed class CoinGeckoClient : IRestDetailCoinClient
{
    private readonly RestApiClient<CoinGeckoRequest> _api;

    private const string GetCoinDescriptionByIdQuery =
        "?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false&sparkline=false";

    public CoinGeckoClient(IOptions<CoinGeckoApiOptions> options)
    {
        _api = new RestApiClient<CoinGeckoRequest>(options.Value);
    }

    public async Task<IEnumerable<CoinBasicInformation>> GetDetailCoinsInformationAsync(string secondCurrency, int page = 1,
        int perPage = 250, bool sparkline = true)
    {
        var strSparkline = sparkline ? "true" : "false";
        var query =
            $"?vs_currency={secondCurrency}&per_page={perPage}&page={page}&sparkline={strSparkline}&price_change_percentage=1h,24h,7d,14d,30d,1y";

        var response = await _api
            .CreateRequest(Method.Get, CoinGeckoEndpoint.CoinMarkets, query)
            .Authenticate()
            .ExecuteAsync();
        var result = response.FromJson<List<CoinBasicInformation>>();

        return result;
    }

    public async Task<CoinDetailedInformation> GetDetailCoinInformationById(string id)
    {
        var response = await _api
            .CreateRequest(Method.Get, CoinGeckoEndpoint.CoinDataById(id), GetCoinDescriptionByIdQuery)
            .Authenticate()
            .ExecuteAsync();

        var result = response.FromJson<CoinDetailedInformation>();

        return result;
    }

    public async Task<MarketChart> GetMarketChartRangeByCoinId(string id, string vsCurrency, string from, string to)
    {
        var query = $"?vs_currency={vsCurrency}&from={from}&to={to}";

        var response = await _api
            .CreateRequest(Method.Get, CoinGeckoEndpoint.CoinHistoricalChartDataTimeRange(id), query)
            .Authenticate()
            .ExecuteAsync();

        var result = response.FromJson<MarketChart>();

        return result;
    }
}