using CryptoPro.CoinsService.Domain.Interfaces;
using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetMarketChartRangeInformation;

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