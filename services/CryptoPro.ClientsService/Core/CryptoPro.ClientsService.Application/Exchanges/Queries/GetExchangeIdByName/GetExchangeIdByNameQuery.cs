using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Exchanges.Queries.GetExchangeIdByName;

public sealed record GetExchangeIdByNameQuery(ExchangeType Exchange) : IRequest<int>;