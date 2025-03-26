using CryptoPro.ExchangeClients.Infrastructure.Common;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Options;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Requests;
using RestSharp;

namespace CryptoPro.ExchangeClients.Infrastructure.RestAPI;

public class BaseRestApi<T> 
    where T : BaseRequest, new()
{
    private readonly ExchangeApiOptions _apiOptions;

    public BaseRestApi(ExchangeApiOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }
    
    public T CreateRequest(Method method, BaseType endpoint,
        string? query = null, string? payload = null)
    {
        var full = endpoint.Value + query;
        var result = new T
        {
            ApiOptions = _apiOptions,
            Client = new RestClient(_apiOptions.BaseUri),
            RequestOptions = new RequestOptions
            {
                FullPath = full,
                Endpoint = endpoint,
                Payload = payload,
                Request = new RestRequest(full)
                {
                    Method = method
                }
            }
        };
        
        return result;
    }
}