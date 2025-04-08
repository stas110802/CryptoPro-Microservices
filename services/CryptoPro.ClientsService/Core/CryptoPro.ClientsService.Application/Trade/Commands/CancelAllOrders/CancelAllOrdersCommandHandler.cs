using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Commands.CancelAllOrders;

public sealed class CancelAllOrdersCommandHandler : IRequestHandler<CancelAllOrdersCommand, bool>
{
    private readonly IEnumerable<IRestTradeClient> _clients;

    public CancelAllOrdersCommandHandler(IEnumerable<IRestTradeClient> clients)
    {
        _clients = clients;
    }

    public async Task<bool> Handle(CancelAllOrdersCommand request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);
        var isSuccessful = await selectedClient.CancelAllOrdersAsync();

        return isSuccessful;
    }
}