using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Application.Interfaces;

public interface IExchangeClientFactory
{
    Task<IRestAccountClient> CreateRestAccountClientAsync(ExchangeType exchange, int userId);
    Task<IRestTradeClient> CreateRestTradeClientAsync(ExchangeType exchange, int userId);
    Task<IRestMarketClient> CreateMarketClientAsync(ExchangeType exchange, int userId);
}