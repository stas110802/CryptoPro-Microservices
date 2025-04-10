using CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;
using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Account.Queries.GetCurrencyAccountBalance;

public sealed class
    GetCurrencyAccountBalanceQueryHandler : IRequestHandler<GetCurrencyAccountBalanceQuery, CurrencyBalance>
{
    private readonly IExchangeClientFactory _exchangeClientFactory;
    private readonly IApiSettingsRepository _apiSettingsRepository;

    public GetCurrencyAccountBalanceQueryHandler(
        IExchangeClientFactory exchangeClientFactory,
        IApiSettingsRepository apiSettingsRepository)
    {
        _exchangeClientFactory = exchangeClientFactory;
        _apiSettingsRepository = apiSettingsRepository;
    }

    public async Task<CurrencyBalance> Handle(GetCurrencyAccountBalanceQuery request,
        CancellationToken cancellationToken)
    {
        var client = await _exchangeClientFactory.CreateRestAccountClientAsync(request.Exchange, request.UserId);
        var balance = await client.GetCurrencyAccountBalanceAsync(request.Currency);

        return balance;
    }
}