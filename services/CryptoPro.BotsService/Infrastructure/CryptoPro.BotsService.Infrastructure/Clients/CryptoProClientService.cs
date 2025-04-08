using System.Globalization;
using System.Net.Http.Json;
using CryptoPro.BotsService.Domain;

namespace CryptoPro.BotsService.Infrastructure.Clients;

public sealed class CryptoProClientService : ICryptoProClientService
{
    private string? _key;
    private readonly HttpClient _httpClient;

    private const string BaseUrl = "http://localhost:5257/";

    public CryptoProClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void SetKey(string key)
    {
        _key = key;
    }

    public async Task<decimal> GetCurrencyPriceAsync(string currency, CancellationToken cancellationToken)
    {
        var response =
            await _httpClient.GetAsync($"{BaseUrl}api/market/binance/getPrice/{currency}", cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<PriceRequest>(cancellationToken: cancellationToken))
            ?.Price ?? 0;

        return result;
    }

    public Task<bool> GetCurrencyAccountBalance(string currency, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateSellOrderAsync(string currency, decimal amount, CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "currency", currency },
            { "amount", amount.ToString(CultureInfo.InvariantCulture) },
            { "price", "0" }
        };
        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
        var requestUri = $"{BaseUrl}api/trade/binance/sellOrder?{queryString}";
        var response = await _httpClient.PostAsync(requestUri, null, cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<StatusRequest>(cancellationToken: cancellationToken))
            ?.Status ?? false;

        return result;
    }
}

public record StatusRequest(bool Status);

public record PriceRequest(decimal Price);