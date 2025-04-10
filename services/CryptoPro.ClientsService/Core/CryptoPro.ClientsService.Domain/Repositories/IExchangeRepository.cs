using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Domain.Repositories;

public interface IExchangeRepository
{
    Task<IEnumerable<ExchangeEntity>> GetAllExchangesAsync();
    Task<int> GetExchangeIdByTypeAsync(ExchangeType exchangeType);
    Task<bool> AddExchangeAsync(ExchangeEntity exchange);
    Task<bool> DeleteExchangeAsync(int exchangeId);
}