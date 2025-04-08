using CryptoPro.BotsService.Domain;

namespace CryptoPro.BotsService.Infrastructure;

public class CryptoProClientService : ICryptoProClientService
{
    public Task<decimal> GetCurrencyPriceAsync(string currency)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateSellOrderAsync(string currency, decimal amount, decimal price)
    {
        throw new NotImplementedException();
    }
}