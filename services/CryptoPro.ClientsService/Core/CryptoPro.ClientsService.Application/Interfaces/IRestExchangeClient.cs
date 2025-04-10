using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Application.Interfaces;

public interface IRestExchangeClient : IRestAccountClient, IRestMarketClient, IRestTradeClient
{
    ExchangeType GetExchangeType();
}