using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;

public sealed record GetAccountBalanceQuery(ExchangeType Exchange) : IRequest<IEnumerable<CurrencyBalance>>;