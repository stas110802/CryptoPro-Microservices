using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Requests;
using RestSharp;

namespace CryptoPro.ExchangeClients.Infrastructure.Clients.Rest.CoinGecko;

public class CoinGeckoRequest : BaseRequest
{
    public override BaseRequest Authenticate(Dictionary<string, object>? bodyParameters = null)
    {
        if (RequestOptions == null || ApiOptions == null)
            throw new NullReferenceException("RequestOptions or ApiOptions cannot be null");
        
        var method = RequestOptions.Request.Method;
        RequestOptions.Request = new RestRequest(RequestOptions.FullPath, method);
        RequestOptions.Request.AddHeader("accept", "application/json");
        RequestOptions.Request.AddHeader("x-cg-demo-api-key", ApiOptions.PublicKey);
        
        return this;
    }
}