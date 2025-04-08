using CryptoPro.ClientsService.Infrastructure.Abstractions;
using CryptoPro.ClientsService.Infrastructure.Options;
using RestSharp;

namespace CryptoPro.ClientsService.Infrastructure.Clients.Rest;

public class RestApiClient<T>
    where T : BaseRequest, new()
{
    private readonly BinanceApiOptions _apiOptions;

    public RestApiClient(BinanceApiOptions apiOptions)
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