using CryptoPro.ExchangeClients.Domain.Clients.Models.Common;

namespace CryptoPro.ExchangeClients.Domain.Clients;

public interface IRestMarketClient
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