using CryptoPro.ClientsService.Domain.Clients.Models;

namespace CryptoPro.ClientsService.Domain.Clients.Interfaces;

public interface IRestTradeClient : IRestExchangeClient
{
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
    /// Return all sell orders
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Order>> GetMyOrdersAsync();
}