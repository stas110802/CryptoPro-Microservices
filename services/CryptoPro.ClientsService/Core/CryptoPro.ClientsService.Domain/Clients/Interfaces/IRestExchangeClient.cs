using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Domain.Clients.Interfaces;

public interface IRestExchangeClient
{
    ExchangeType GetExchangeType();
}