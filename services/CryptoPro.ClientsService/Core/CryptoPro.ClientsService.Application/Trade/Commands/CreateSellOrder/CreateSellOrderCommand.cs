using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;

public sealed record CreateSellOrderCommand(ExchangeType Exchange, string Currency, decimal Amount, decimal Price, int UserId) : IRequest<bool>;