using CryptoPro.ClientsService.Application.Interfaces;
using MediatR;

namespace CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;

public sealed class CreateSellOrderCommandHandler : IRequestHandler<CreateSellOrderCommand, bool>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public CreateSellOrderCommandHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<bool> Handle(CreateSellOrderCommand request, CancellationToken cancellationToken)
    {
        var client = await _exchangeClientFactory.CreateRestTradeClientAsync(request.Exchange, request.UserId);
        
        if (request.Price == 0)
        {
            return await client.CreateSellOrderAsync(request.Currency, request.Amount);
        }
        
        return await client.CreateSellOrderAsync(request.Currency, request.Amount, request.Price);
    }
}