using CryptoPro.ExchangeClients.Infrastructure.Common;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Options;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Requests;
using RestSharp;

namespace CryptoPro.ExchangeClients.Infrastructure.RestAPI;

public class RestApiClient<T>
    where T : BaseRequest, new()
{
    private readonly ExchangeApiOptions _apiOptions;

    public RestApiClient(ExchangeApiOptions apiOptions)
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