namespace CryptoPro.BotsService.Domain;

public interface ICryptoProClientService
{
    Task<decimal> GetCurrencyPriceAsync(string currency);
    Task<bool> CreateSellOrderAsync(string currency, decimal amount, decimal price);
}