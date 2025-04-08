using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyInformation;

public sealed record GetCurrencyInformationQuery(ExchangeType Exchange, string Currency) : IRequest<CurrencyPair>;