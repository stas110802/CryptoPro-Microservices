using CryptoPro.ClientsService.Infrastructure.Abstractions;
using CryptoPro.ClientsService.Infrastructure.Helpers;
using RestSharp;

namespace CryptoPro.ClientsService.Infrastructure.Clients.Rest.Binance;

public class BinanceRequest : BaseRequest
{
    public override BaseRequest Authenticate(Dictionary<string, object>? bodyParameters = null)
    {
        if (RequestOptions == null || ApiOptions == null)
            throw new NullReferenceException("RequestOptions or ApiOptions cannot be null");
        
        var query = GetQuery(RequestOptions.FullPath);
        if (string.IsNullOrEmpty(query))
            throw new NullReferenceException("[Binance Request Error]\nRequired query parameters cant be nullable");
        
        var signature = HashCalculator.CalculateHMACSHA256Hash(query, ApiOptions.SecretKey);
        RequestOptions.FullPath += $"&signature={signature}";
        var method = RequestOptions.Request.Method;
        RequestOptions.Request = new RestRequest(RequestOptions.FullPath, method);
        RequestOptions.Request.AddHeader("X-MBX-APIKEY", ApiOptions.PublicKey);
        
        return this;
    }
    
    private static string? GetQuery(string url)
    {
        var arrSplit = url.Split('?');

        return arrSplit.Length == 1 ? null : arrSplit[1];
    }
}