using CryptoPro.ClientsService.Infrastructure.Abstractions;
using CryptoPro.ClientsService.Infrastructure.Options;
using RestSharp;

namespace CryptoPro.ClientsService.Infrastructure.Clients.Rest;

public class RestApiClient<T>
    where T : BaseRequest, new()
{
    private readonly BinanceApiOptions _binanceApiOptions;

    public RestApiClient(BinanceApiOptions binanceApiOptions)
    {
        _binanceApiOptions = binanceApiOptions;
    }

    public T CreateRequest(Method method, BaseType endpoint,
        string? query = null, string? payload = null)
    {
        var full = endpoint.Value + query;
        return new T
        {
            ApiOptions = _binanceApiOptions,
            Client = new RestClient(_binanceApiOptions.BaseUri),
            RequestOptions = RequestOptions.Create(method, endpoint, full, payload)
        };
    }
}