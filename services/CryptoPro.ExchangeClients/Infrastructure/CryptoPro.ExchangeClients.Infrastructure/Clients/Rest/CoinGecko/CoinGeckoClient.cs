using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using CryptoPro.ExchangeClients.Infrastructure.Common;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Options;
using RestSharp;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CryptoPro.ExchangeClients.Infrastructure.Clients.Rest.CoinGecko;

public sealed class CoinGeckoClient : IRestDetailCoinClient
{
    private readonly RestApiClient<CoinGeckoRequest> _api;

    private const string GetCoinDescriptionByIdQuery =
        "?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false&sparkline=false";

    public CoinGeckoClient(IOptions<ExchangeApiOptions> options)
    {
        _api = new RestApiClient<CoinGeckoRequest>(options.Value);
    }

    public async Task<IEnumerable<CoinInformation>> GetDetailCoinsInformationAsync(string secondCurrency, int page = 1,
        int perPage = 250, bool sparkline = true)
    {
        var strSparkline = sparkline ? "true" : "false";
        var query =
            $"?vs_currency={secondCurrency}&per_page={perPage}&page={page}&sparkline={strSparkline}&price_change_percentage=1h,24h,7d,14d,30d,1y";

        var response = await _api
            .CreateRequest(Method.Get, CoinGeckoEndpoint.CoinMarkets, query)
            .Authenticate()
            .ExecuteAsync();
        var result = response.FromJson<List<CoinInformation>>();

        return result;
    }

    public async Task<CoinFullDataById> GetDetailCoinInformationById(string id)
    {
        var response = await _api
            .CreateRequest(Method.Get, CoinGeckoEndpoint.CoinDataById(id), GetCoinDescriptionByIdQuery)
            .Authenticate()
            .ExecuteAsync();

        var result = response.FromJson<CoinFullDataById>();

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