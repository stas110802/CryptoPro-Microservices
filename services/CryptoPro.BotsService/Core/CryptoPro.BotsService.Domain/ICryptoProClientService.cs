namespace CryptoPro.BotsService.Domain;

public interface ICryptoProClientService
{
    void SetKey(string key);
    Task<decimal> GetCurrencyPriceAsync(string currency, CancellationToken cancellationToken);
    Task<bool> CreateSellOrderAsync(string currency, decimal amount, CancellationToken cancellationToken);
    Task<bool> GetCurrencyAccountBalance(string currency, CancellationToken cancellationToken);
}