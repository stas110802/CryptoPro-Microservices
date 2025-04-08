using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyPrice;

public sealed class GetCurrencyPriceQueryHandler : IRequestHandler<GetCurrencyPriceQuery, decimal>
{
    private readonly IEnumerable<IRestMarketClient> _clients;

    public GetCurrencyPriceQueryHandler(IEnumerable<IRestMarketClient> clients)
    {
        _clients = clients;
    }

    public async Task<decimal> Handle(GetCurrencyPriceQuery request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);

        var price = await selectedClient.GetCurrencyPriceAsync(request.Currency);

        return price;
    }
}