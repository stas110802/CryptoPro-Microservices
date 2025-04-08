using CryptoPro.CoinsService.Infrastructure.Abstractions;
using CryptoPro.CoinsService.Infrastructure.Options;
using RestSharp;

namespace CryptoPro.CoinsService.Infrastructure.Rest;

public class RestApiClient<T>
    where T : BaseRequest, new()
{
    private readonly CoinGeckoApiOptions _apiOptions;

    public RestApiClient(CoinGeckoApiOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }

    public T CreateRequest(Method method, BaseType endpoint,
        string? query = null, string? payload = null)
    {
        var full = endpoint.Value + query;
        return new T
        {
            ApiOptions = _apiOptions,
            Client = new RestClient(_apiOptions.BaseUri),
            RequestOptions = RequestOptions.Create(method, endpoint, full, payload)
        };
    }
}