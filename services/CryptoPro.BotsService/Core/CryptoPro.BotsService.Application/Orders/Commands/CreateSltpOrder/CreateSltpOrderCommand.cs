using CryptoPro.BotsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.BotsService.Application.Orders.Commands.CreateSltpOrder;

public sealed record CreateSltpOrderCommand(SltpOrderCreateDto Order) : IRequest<bool>;
