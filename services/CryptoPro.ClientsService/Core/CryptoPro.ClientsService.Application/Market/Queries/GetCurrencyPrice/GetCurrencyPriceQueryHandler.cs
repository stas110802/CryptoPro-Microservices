using CryptoPro.ClientsService.Application.Interfaces;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyPrice;

public sealed class GetCurrencyPriceQueryHandler : IRequestHandler<GetCurrencyPriceQuery, decimal>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetCurrencyPriceQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<decimal> Handle(GetCurrencyPriceQuery request, CancellationToken cancellationToken)
    {
        var client = await _exchangeClientFactory.CreateMarketClientAsync(request.Exchange, request.UserId);
        var price = await client.GetCurrencyPriceAsync(request.Currency);

        return price;
    }
}