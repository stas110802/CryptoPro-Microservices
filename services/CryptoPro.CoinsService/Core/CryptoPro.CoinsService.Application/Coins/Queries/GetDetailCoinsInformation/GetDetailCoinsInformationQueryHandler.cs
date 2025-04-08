using CryptoPro.CoinsService.Domain.Interfaces;
using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinsInformation;

public sealed class GetDetailCoinsInformationQueryHandler  
    : IRequestHandler<GetDetailCoinsInformationQuery, IEnumerable<CoinBasicInformation>>
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public GetDetailCoinsInformationQueryHandler(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }
    
    public async Task<IEnumerable<CoinBasicInformation>> Handle(GetDetailCoinsInformationQuery request, CancellationToken cancellationToken)
    {
        var content = await _detailCoinClient.GetDetailCoinsInformationAsync(request.RegularCurrency, request.Page);
        
        return content;
    }
}