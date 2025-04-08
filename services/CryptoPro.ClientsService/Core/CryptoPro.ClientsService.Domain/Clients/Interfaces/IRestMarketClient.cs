using CryptoPro.ClientsService.Domain.Clients.Models;

namespace CryptoPro.ClientsService.Domain.Clients.Interfaces;

public interface IRestMarketClient : IRestExchangeClient
{
    /// <summary>
    /// Returns information about a currency pair
    /// </summary>
    /// <param name="currency">Currency pair</param>
    /// <returns></returns>
    Task<CurrencyPair> GetCurrencyInformationAsync(string currency);

    /// <summary>
    /// Returns the current trading value of a currency
    /// </summary>
    /// <param name="currency">Currency pair</param>
    /// <returns></returns>
    Task<decimal> GetCurrencyPriceAsync(string currency);

}