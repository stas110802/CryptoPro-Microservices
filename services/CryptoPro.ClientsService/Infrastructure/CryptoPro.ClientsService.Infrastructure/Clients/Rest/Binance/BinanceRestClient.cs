using System.Globalization;
using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.ClientsService.Infrastructure.Extensions;
using CryptoPro.ClientsService.Infrastructure.Options;
using CryptoPro.ExchangeClients.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Decimal;

namespace CryptoPro.ClientsService.Infrastructure.Clients.Rest.Binance;

public sealed class BinanceRestClient : IRestExchangeClient
{
    private readonly RestApiClient<BinanceRequest> _api;

    public BinanceRestClient(BinanceApiOptions options)
    {
        _api = new RestApiClient<BinanceRequest>(options);
    }

    public async Task<CurrencyPair> GetCurrencyInformationAsync(string currency)
    {
        var query = GetSymbolQuery(currency);
        var response = (await _api
                .CreateRequest(Method.Get, BinanceEndpoint.CurrencyInfo, query)
                .ExecuteAsync())
            .FromJson<JToken>();

        return new CurrencyPair
        {
            Currency = currency,
            SellingPrice = Parse(response["lastPrice"].ToString(), CultureInfo.InvariantCulture),
            TradingVolume = Parse(response["volume"].ToString(), CultureInfo.InvariantCulture)
        };
    }

    public async Task<decimal> GetCurrencyPriceAsync(string currency)
    {
        var query = GetSymbolQuery(currency);
        var response = await _api
            .CreateRequest(Method.Get, BinanceEndpoint.CurrentPrice, query)
            .ExecuteAsync();

        var strPrice = response.FromJson<JToken>()["price"];
        if (strPrice == null)
            throw new NullReferenceException("Binance.GetCurrencyPrice response is null");

        var price = Parse(strPrice.ToString(), CultureInfo.InvariantCulture);

        return price;
    }

    public async Task<IEnumerable<CurrencyBalance>> GetAccountBalanceAsync()
    {
        var balances = new List<CurrencyBalance>();
        var timestamp = TimestampHelper.GetUtcTimestamp();
        var query = $"?timestamp={timestamp}&omitZeroBalances=true";
        var response = (await _api
                .CreateRequest(Method.Get, BinanceEndpoint.AccountInfo, query)
                .Authenticate()
                .ExecuteAsync())
            .FromJson<JToken>();

        var accountBalances = response["balances"];
        if (accountBalances == null)
            return balances;

        balances.AddRange(from coin in accountBalances
            let currency = coin["asset"].ToString()
            let freeBalance = Parse(coin["free"].ToString(), CultureInfo.InvariantCulture)
            let lockedBalance = Parse(coin["locked"].ToString(), CultureInfo.InvariantCulture)
            select new CurrencyBalance
                { Currency = currency, AvailableBalance = freeBalance, LockedBalance = lockedBalance });

        return balances;
    }

    public async Task<CurrencyBalance> GetCurrencyAccountBalanceAsync(string currency)
    {
        var balances = await GetAccountBalanceAsync();
        foreach (var item in balances)
        {
            if (item.Currency == currency)
                return item;
        }

        return new CurrencyBalance { Currency = currency, AvailableBalance = 0 };
    }

    public async Task<bool> CreateSellOrderAsync(string currency, decimal amount, decimal price)
    {
        var strAmount = amount.ToString(CultureInfo.InvariantCulture);
        var strPrice = price.ToString(CultureInfo.InvariantCulture);
        var query =
            $"?timestamp={GetTimestamp()}&symbol={currency}&quantity={strAmount}&price={strPrice}&side=SELL&type=LIMIT&timeInForce=GTC";

        return await CreateSellOrderAsync(query);
    }

    public async Task<bool> CreateSellOrderAsync(string currency, decimal amount)
    {
        var strAmount = amount.ToString(CultureInfo.InvariantCulture);
        var query = $"?timestamp={GetTimestamp()}&symbol={currency}&quantity={strAmount}&side=SELL&type=MARKET";

        return await CreateSellOrderAsync(query);
    }

    public async Task<bool> CancelAllOrdersAsync()
    {
        var myOrders = await GetMyOrdersAsync();
        foreach (var order in myOrders)
        {
            var query = $"{GetSymbolQuery(order.Currency)}&orderId={order.OrderId}";
            var response = await _api
                .CreateRequest(Method.Delete, BinanceEndpoint.Order, query)
                .Authenticate()
                .ExecuteAsync();

            var orderStatus = response.FromJson<JToken>()["status"]?.ToString();
            if (orderStatus == "CANCELED")
                continue;

            return false;
        }

        return true;
    }

    public async Task<bool> WithdrawalCurrencyAsync(string coin, decimal amount, string address)
    {
        var timestamp = TimestampHelper.GetUtcTimestamp();
        var query = $"?timestamp={timestamp}&coin={coin}&address={address}&amount={amount}";

        var withdrawalResponse = (await _api
                .CreateRequest(Method.Post, BinanceEndpoint.Withdraw, query)
                .Authenticate()
                .ExecuteAsync())
            .FromJson<JToken>();

        var id = withdrawalResponse["id"]?.ToString();
        var isSuccessful = TryParse(id, CultureInfo.InvariantCulture, out _);

        return isSuccessful;
    }

    public async Task<IEnumerable<Order>> GetMyOrdersAsync()
    {
        var query = $"?timestamp={GetTimestamp()}";
        var result = new List<Order>();
        var response = await _api
            .CreateRequest(Method.Get, BinanceEndpoint.OrderList, query)
            .Authenticate()
            .ExecuteAsync();

        var orders = response.FromJson<JToken>()["orders"];
        if (orders == null)
            return result;

        foreach (var order in orders)
        {
            result.Add(new Order
            {
                Currency = order["symbol"].ToString(),
                OrderId = Parse(order["orderId"].ToString(), CultureInfo.InvariantCulture)
            });
        }

        return result;
    }

    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Binance;
    }

    private string GetTimestamp() => TimestampHelper.GetUtcTimestamp();

    private static string GetSymbolQuery(string currency)
    {
        return $"?symbol={currency.ToUpper()}";
    }

    private async Task<bool> CreateSellOrderAsync(string query)
    {
        var response = await _api
            .CreateRequest(Method.Post, BinanceEndpoint.Order, query)
            .Authenticate()
            .ExecuteAsync();

        var id = response.FromJson<JToken>()["orderId"]?.ToString();

        return TryParse(id, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
    }
}