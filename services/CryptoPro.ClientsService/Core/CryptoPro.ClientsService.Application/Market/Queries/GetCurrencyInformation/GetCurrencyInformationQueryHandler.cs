using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyInformation;

public sealed class GetCurrencyInformationQueryHandler : IRequestHandler<GetCurrencyInformationQuery, CurrencyPair>
{
    private readonly IEnumerable<IRestMarketClient> _clients;

    public GetCurrencyInformationQueryHandler(IEnumerable<IRestMarketClient> clients)
    {
        _clients = clients;
    }

    public async Task<CurrencyPair> Handle(GetCurrencyInformationQuery request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);

        var currencyPair = await selectedClient.GetCurrencyInformationAsync(request.Currency);

        return currencyPair;
    }
}