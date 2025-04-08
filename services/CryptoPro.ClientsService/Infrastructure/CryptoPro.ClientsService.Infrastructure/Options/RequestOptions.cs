using CryptoPro.ClientsService.Infrastructure.Abstractions;
using RestSharp;

namespace CryptoPro.ClientsService.Infrastructure.Options;

public class RequestOptions
{
    private RequestOptions(RestRequest request, BaseType endpoint, string fullPath, string? payload)
    {
        Request = request;
        Endpoint = endpoint;
        FullPath = fullPath;
        Payload = payload;
    }

    public RestRequest Request { get; set; }
    public BaseType Endpoint { get; set; }
    public string FullPath { get; set; }
    public string? Payload { get; set; }


    public static RequestOptions Create(Method method, BaseType endpoint, string fullPath, string? payload)
    {
        var restRequest = new RestRequest(fullPath, method);

        return new RequestOptions(restRequest, endpoint, fullPath, payload);
    }
}