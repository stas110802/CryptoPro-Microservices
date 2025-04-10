using CryptoPro.ClientsService.Domain.Clients.Models;

namespace CryptoPro.ClientsService.Application.Interfaces;

public interface IRestAccountClient 
{
    /// <summary>
    /// Returns a list of coins and their amount on the account
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CurrencyBalance>> GetAccountBalanceAsync(); 
    
    /// <summary>
    /// Returns the currency balance on the account
    /// </summary>
    /// <param name="currency">Solo currency (BTC, USDT, LTC etc...)</param>
    /// <returns></returns>
    Task<CurrencyBalance> GetCurrencyAccountBalanceAsync(string currency);
    
    /// <summary>
    /// Withdrawal currency
    /// </summary>
    /// <returns></returns>
    Task<bool> WithdrawalCurrencyAsync(string coin, decimal amount, string address);
}