using CryptoPro.BotsService.Domain.Entities;
using MediatR;

namespace CryptoPro.BotsService.Application.Orders.Queries.GetUserSltpOrders;

public sealed record GetUserSltpOrdersQuery(int UserId) : IRequest<IEnumerable<SltpOrderEntity>>;