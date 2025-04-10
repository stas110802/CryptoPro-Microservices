using CryptoPro.BotsService.Domain.Types;

namespace CryptoPro.BotsService.Domain;

public interface ICryptoProClientService
{
    void SetUserId(int userId);
    void SetExchange(ExchangeType exchange);
    Task<decimal> GetCurrencyPriceAsync(string currency, CancellationToken cancellationToken);
    Task<bool> CreateSellOrderAsync(string currency, decimal amount, CancellationToken cancellationToken);
    Task<decimal> GetCurrencyAccountBalance(string currency, CancellationToken cancellationToken);
    Task<int> GetExchangeIdByType(ExchangeType exchangeType, CancellationToken cancellationToken);
}