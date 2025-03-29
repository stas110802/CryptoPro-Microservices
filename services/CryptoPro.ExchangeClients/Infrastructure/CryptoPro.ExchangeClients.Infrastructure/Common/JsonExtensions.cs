using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.Infrastructure.Common;

public static class JsonExtensions
{
    public static T FromJson<T>(this string json)
    {
        var result = JsonConvert.DeserializeObject<T>(json, JsonSettings);
        if (result is null)
            throw new NullReferenceException("[ExchangeClients.JsonExtensions, method=FromJson] result is null");

        return result;
    }
    
    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };
}