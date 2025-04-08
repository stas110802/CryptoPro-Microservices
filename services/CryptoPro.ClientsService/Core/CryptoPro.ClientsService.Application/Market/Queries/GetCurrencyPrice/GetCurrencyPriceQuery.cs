using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyPrice;

public sealed record GetCurrencyPriceQuery(ExchangeType Exchange, string Currency) : IRequest<decimal>;
