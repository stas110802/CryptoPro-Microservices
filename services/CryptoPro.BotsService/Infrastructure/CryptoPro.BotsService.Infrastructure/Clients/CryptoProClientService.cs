using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Types;
using CryptoPro.BotsService.Infrastructure.Redis;

namespace CryptoPro.BotsService.Infrastructure.Clients;

public sealed class CryptoProClientService : ICryptoProClientService
{
    private readonly HttpClient _httpClient;
    private readonly IRedisService _cache;
    private int _userId;
    private ExchangeType _exchange;
    private const string BaseUrl = "http://localhost:5257/";

    public CryptoProClientService(HttpClient httpClient, IRedisService cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public void SetUserId(int userId)
    {
        _userId = userId;
    }

    public void SetExchange(ExchangeType exchange)
    {
        _exchange = exchange;
    }

    public async Task<decimal> GetCurrencyPriceAsync(string currency, CancellationToken cancellationToken)
    {
        var response =
            await _httpClient.GetAsync($"{BaseUrl}api/market/{GetExchangeName()}/getPrice/{currency}",
                cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<PriceRequest>(cancellationToken: cancellationToken))
            ?.Price ?? 0;

        return result;
    }

    public async Task<decimal> GetCurrencyAccountBalance(string currency, CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "currency", currency }
        };
        await SetJwtTokenInHeader();

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
        var requestUri = $"{BaseUrl}api/account/{GetExchangeName()}/currencyBalance?{queryString}";
        var response = await _httpClient.PostAsync(requestUri, null, cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<BalanceRequest>(cancellationToken: cancellationToken))
            ?.AvailableBalance ?? 0;

        return result;
    }

    public async Task<int> GetExchangeIdByType(ExchangeType exchangeType, CancellationToken cancellationToken)
    {
        var requestUri = $"{BaseUrl}api/exchanges/getIdByName/{GetExchangeName()}";
        var response = await _httpClient.PostAsync(requestUri, null, cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<ExchangeRequest>(cancellationToken: cancellationToken))
            ?.Id ?? 0;

        return result;
    }

    public async Task<bool> CreateSellOrderAsync(string currency, decimal amount, CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "currency", currency },
            { "amount", amount.ToString(CultureInfo.InvariantCulture) },
            { "price", "0" }
        };
        await SetJwtTokenInHeader();

        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
        var requestUri = $"{BaseUrl}api/trade/binance/sellOrder?{queryString}";
        var response = await _httpClient.PostAsync(requestUri, null, cancellationToken);
        var result = (await response.Content.ReadFromJsonAsync<StatusRequest>(cancellationToken: cancellationToken))
            ?.Status ?? false;

        return result;
    }

    private async Task SetJwtTokenInHeader()
    {
        var token = await _cache.GetAsync($"jwt:{_userId}");
        if (token == null)
            throw new ArgumentException("UserId not fount");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private string GetExchangeName() => _exchange.ToString().ToLower();
}

public record StatusRequest(bool Status);

public record PriceRequest(decimal Price);

public record BalanceRequest(string Currency, decimal AvailableBalance, decimal LockedBalance, decimal TotalBalance);

public record ExchangeRequest(int Id);