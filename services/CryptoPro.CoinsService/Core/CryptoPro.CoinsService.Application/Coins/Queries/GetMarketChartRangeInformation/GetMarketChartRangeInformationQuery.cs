using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetMarketChartRangeInformation;

public sealed record GetMarketChartRangeInformationQuery(string Id, string VsCurrency, string From, string To)
    : IRequest<MarketChart>;