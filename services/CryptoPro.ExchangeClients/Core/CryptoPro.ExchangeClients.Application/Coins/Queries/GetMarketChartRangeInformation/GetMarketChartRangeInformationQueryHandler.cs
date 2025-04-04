using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetMarketChartRangeInformation;

public sealed class GetMarketChartRangeInformationQueryHandler
    : IRequestHandler<GetMarketChartRangeInformationQuery, MarketChart>
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public GetMarketChartRangeInformationQueryHandler(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }

    public async Task<MarketChart> Handle(GetMarketChartRangeInformationQuery request,
        CancellationToken cancellationToken)
    {
        var chart = await _detailCoinClient.GetMarketChartRangeByCoinId(request.Id, request.VsCurrency, request.From,
            request.To);

        return chart;
    }
}