using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinsInformation;

public sealed class GetDetailCoinsInformationQueryHandler  
    : IRequestHandler<GetDetailCoinsInformationQuery, IEnumerable<CoinInformation>>
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public GetDetailCoinsInformationQueryHandler(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }
    
    public async Task<IEnumerable<CoinInformation>> Handle(GetDetailCoinsInformationQuery request, CancellationToken cancellationToken)
    {
        var content = await _detailCoinClient.GetDetailCoinsInformationAsync(request.RegularCurrency, request.Page);
        
        return content;
    }
}