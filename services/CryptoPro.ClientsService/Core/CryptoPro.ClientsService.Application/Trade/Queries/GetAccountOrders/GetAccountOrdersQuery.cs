using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Queries.GetAccountOrders;

public sealed record GetAccountOrdersQuery(ExchangeType Exchange) : IRequest<IEnumerable<Order>>;