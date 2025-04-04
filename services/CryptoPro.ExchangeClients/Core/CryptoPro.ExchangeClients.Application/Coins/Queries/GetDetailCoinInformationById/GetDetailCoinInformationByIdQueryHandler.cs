using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinInformationById;

public class GetDetailCoinInformationByIdQueryHandler
    : IRequestHandler<GetDetailCoinInformationByIdQuery, CoinFullDataById>
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public GetDetailCoinInformationByIdQueryHandler(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }

    public async Task<CoinFullDataById> Handle(GetDetailCoinInformationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _detailCoinClient.GetDetailCoinInformationById(request.Id);

        return data;
    }
}