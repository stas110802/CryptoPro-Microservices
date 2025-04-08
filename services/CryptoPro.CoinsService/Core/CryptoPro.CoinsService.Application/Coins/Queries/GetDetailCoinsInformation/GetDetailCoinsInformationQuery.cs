using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinsInformation;

public sealed record GetDetailCoinsInformationQuery(string RegularCurrency, int Page) : IRequest<IEnumerable<CoinBasicInformation>>;
