using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Queries.GetAccountOrders;

public sealed class GetAccountOrdersQueryHandler : IRequestHandler<GetAccountOrdersQuery, IEnumerable<Order>>
{
    private readonly IEnumerable<IRestTradeClient> _clients;

    public GetAccountOrdersQueryHandler(IEnumerable<IRestTradeClient> clients)
    {
        _clients = clients;
    }

    public async Task<IEnumerable<Order>> Handle(GetAccountOrdersQuery request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);
        var orders = await selectedClient.GetMyOrdersAsync();

        return orders;
    }
}