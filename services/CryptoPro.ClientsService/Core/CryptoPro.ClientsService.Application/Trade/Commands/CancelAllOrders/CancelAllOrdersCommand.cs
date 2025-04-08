using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Commands.CancelAllOrders;

public sealed record CancelAllOrdersCommand(ExchangeType Exchange) : IRequest<bool>;