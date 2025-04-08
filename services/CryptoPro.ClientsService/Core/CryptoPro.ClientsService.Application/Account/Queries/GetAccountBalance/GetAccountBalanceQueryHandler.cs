using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;

public sealed class GetAccountBalanceQueryHandler: IRequestHandler<GetAccountBalanceQuery, IEnumerable<CurrencyBalance>>
{
    private readonly IEnumerable<IRestAccountClient> _clients;

    public GetAccountBalanceQueryHandler(IEnumerable<IRestAccountClient> clients)
    {
        _clients = clients;
    }

    public async Task<IEnumerable<CurrencyBalance>> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        var selectedClient = _clients
            .Single(c
                => c.GetExchangeType() == request.Exchange);

        var currencyBalances = await selectedClient.GetAccountBalanceAsync();

        return currencyBalances;
    }
}