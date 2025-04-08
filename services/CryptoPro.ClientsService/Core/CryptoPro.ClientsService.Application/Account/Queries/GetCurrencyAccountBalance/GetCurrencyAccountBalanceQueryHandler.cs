using CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;
using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetCurrencyAccountBalance;

public sealed class GetCurrencyAccountBalanceQueryHandler: IRequestHandler<GetCurrencyAccountBalanceQuery, CurrencyBalance>
{
    private readonly IEnumerable<IRestAccountClient> _clients;

    public GetCurrencyAccountBalanceQueryHandler(IEnumerable<IRestAccountClient> clients)
    {
        _clients = clients;
    }

    public async Task<CurrencyBalance> Handle(GetCurrencyAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);

        var currencyBalance = await selectedClient.GetCurrencyAccountBalanceAsync(request.Currency);

        return currencyBalance;
    }
}