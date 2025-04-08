using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;

public sealed class CreateSellOrderCommandHandler : IRequestHandler<CreateSellOrderCommand, bool>
{
    private readonly IEnumerable<IRestTradeClient> _clients;

    public CreateSellOrderCommandHandler(IEnumerable<IRestTradeClient> clients)
    {
        _clients = clients;
    }

    public async Task<bool> Handle(CreateSellOrderCommand request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);
        
        if (request.Price == 0)
        {
            return await selectedClient.CreateSellOrderAsync(request.Currency, request.Amount);
        }
        
        return await selectedClient.CreateSellOrderAsync(request.Currency, request.Amount, request.Price);
    }
}