using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinsInformation;

public sealed record GetDetailCoinsInformationQuery(string RegularCurrency, int Page) : IRequest<IEnumerable<CoinInformation>>;
