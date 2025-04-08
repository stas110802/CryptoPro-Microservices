using CryptoPro.ClientsService.Domain.Entities;

namespace CryptoPro.ClientsService.Domain.Repositories;

public interface IExchangeRepository
{
    Task<IEnumerable<ExchangeEntity>> GetAllExchangesAsync();
    Task<bool> AddExchangeAsync(ExchangeEntity exchange);
    Task<bool> DeleteExchangeAsync(int exchangeId);
}