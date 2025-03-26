using CryptoPro.ExchangeClients.Domain.Clients.Models;

namespace CryptoPro.ExchangeClients.Domain.Clients;

public interface IRestMarketClient
{
    /// <summary>
    /// Returns information about a currency pair
    /// </summary>
    /// <param name="currency">Currency pair</param>
    /// <returns></returns>
    Task<CurrencyPair> GetCurrencyInfoAsync(string currency);

    /// <summary>
    /// Returns the current trading value of a currency
    /// </summary>
    /// <param name="currency">Currency pair</param>
    /// <returns></returns>
    Task<decimal> GetCurrencyPriceAsync(string currency);

    /// <summary>
    /// Returns a list of coins and their amount on the account
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CurrencyBalance>> GetAccountBalanceAsync(); 

    /// <summary>
    /// Returns the currency balance on the account
    /// </summary>
    /// <param name="currency">Solo currency</param>
    /// <returns></returns>
    Task<CurrencyBalance> GetCurrencyBalanceAsync(string currency);

    /// <summary>
    /// Creates an order to sell a currency pair
    /// </summary>
    /// <param name="currency">CurrencyPair pair</param>
    /// <param name="amount">Amount to sell</param>
    /// /// <param name="price">Selling price</param>
    /// <returns></returns>
    Task<bool> CreateSellOrderAsync(string currency, decimal amount, decimal price);

    /// <summary>
    /// Create a MARKET order to sell a currency pair
    /// </summary>
    /// <param name="currency">CurrencyPair pair</param>
    /// <param name="amount">Amount to sell</param>
    /// <returns></returns>
    Task<bool> CreateSellOrderAsync(string currency, decimal amount);

    /// <summary>
    /// Cancel all (sell and buy) orders
    /// </summary>
    /// <returns></returns>
    Task<bool> CancelAllOrdersAsync();

    /// <summary>
    /// Withdrawal currency
    /// </summary>
    /// <returns></returns>
    Task<bool> WithdrawalCurrencyAsync(string coin, decimal amount, string address);

    /// <summary>
    /// Return all sell orders
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Order>> GetMyOrdersAsync();
}