using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetMarketChartRangeInformation;

public sealed record GetMarketChartRangeInformationQuery(string Id, string VsCurrency, string From, string To)
    : IRequest<MarketChart>;