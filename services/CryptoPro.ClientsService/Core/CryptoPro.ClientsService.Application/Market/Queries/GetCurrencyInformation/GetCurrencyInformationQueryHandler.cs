using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using MediatR;

namespace CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyInformation;

public sealed class GetCurrencyInformationQueryHandler : IRequestHandler<GetCurrencyInformationQuery, CurrencyPair>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetCurrencyInformationQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<CurrencyPair> Handle(GetCurrencyInformationQuery request, CancellationToken cancellationToken)
    {
        var client = await _exchangeClientFactory.CreateMarketClientAsync(request.Exchange, request.UserId);
        var currencyPair = await client.GetCurrencyInformationAsync(request.Currency);

        return currencyPair;
    }
}