using CryptoPro.CoinsService.Domain.Interfaces;
using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinInformationById;

public class GetDetailCoinInformationByIdQueryHandler
    : IRequestHandler<GetDetailCoinInformationByIdQuery, CoinDetailedInformation>
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public GetDetailCoinInformationByIdQueryHandler(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }

    public async Task<CoinDetailedInformation> Handle(GetDetailCoinInformationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _detailCoinClient.GetDetailCoinInformationById(request.Id);

        return data;
    }
}