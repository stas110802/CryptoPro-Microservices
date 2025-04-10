using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.ClientsService.Infrastructure.Clients.Rest.Binance;
using CryptoPro.ClientsService.Infrastructure.Options;

namespace CryptoPro.ClientsService.Infrastructure.Factories;

public sealed class ExchangeClientFactory : IExchangeClientFactory
{
    private readonly IApiSettingsRepository _apiSettingsRepository;

    public ExchangeClientFactory(IApiSettingsRepository apiSettingsRepository)
    {
        _apiSettingsRepository = apiSettingsRepository;
    }

    public async Task<IRestAccountClient> CreateRestAccountClientAsync(ExchangeType exchange, int userId)
    {
        return await GetAllInterfaceClient(exchange, userId);
    }

    public async Task<IRestTradeClient> CreateRestTradeClientAsync(ExchangeType exchange, int userId)
    {
        return await GetAllInterfaceClient(exchange, userId);
    }

    public async Task<IRestMarketClient> CreateMarketClientAsync(ExchangeType exchange, int userId)
    {
        return await GetAllInterfaceClient(exchange, userId);
    }

    private async Task<IRestExchangeClient> GetAllInterfaceClient(ExchangeType exchange, int userId)
    {
        var settings = await _apiSettingsRepository.GetApiSettingsByUserIdAsync(exchange, userId);
        if (settings is null)
        {
            throw new Exception($"{exchange} settings for '{userId}-user' does not exist");
        }

        if (settings.Exchange == null)
            throw new ArgumentNullException(nameof(settings.Exchange));

        if (settings.Exchange.Type == ExchangeType.Binance)
        {
            return new BinanceRestClient(new BinanceApiOptions
            {
                BaseUri = "https://testnet.binance.vision",
                PublicKey = settings.PublicKey ?? string.Empty,
                SecretKey = settings.SecretKey ?? string.Empty
            });
        }

        return new BinanceRestClient(new BinanceApiOptions { BaseUri = "https://testnet.binance.vision" });
    }
}