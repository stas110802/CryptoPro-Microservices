using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;

public sealed class
    GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, IEnumerable<CurrencyBalance>>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public GetAccountBalanceQueryHandler(IExchangeClientFactory exchangeClientFactory)
    {
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<IEnumerable<CurrencyBalance>> Handle(GetAccountBalanceQuery request,
        CancellationToken cancellationToken)
    {
        var client = await _exchangeClientFactory.CreateRestAccountClientAsync(request.Exchange, request.UserId);
        var currencyBalances = await client.GetAccountBalanceAsync();

        return currencyBalances;
    }
}