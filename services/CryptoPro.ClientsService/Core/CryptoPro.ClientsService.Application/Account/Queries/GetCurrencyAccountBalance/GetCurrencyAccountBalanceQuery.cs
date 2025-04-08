using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetCurrencyAccountBalance;

public sealed record GetCurrencyAccountBalanceQuery(ExchangeType Exchange, string Currency) : IRequest<CurrencyBalance>;
