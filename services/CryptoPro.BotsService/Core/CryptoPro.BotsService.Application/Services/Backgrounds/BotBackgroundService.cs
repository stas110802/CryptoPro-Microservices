using CryptoPro.BotsService.Application.Repositories;
using CryptoPro.BotsService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoPro.BotsService.Application.Services.Backgrounds;

public class BotBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IBotStateRepository _stateRepo;

    public BotBackgroundService(
        IServiceScopeFactory scopeFactory,
        IBotStateRepository stateRepo)
    {
        _scopeFactory = scopeFactory;
        _stateRepo = stateRepo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var exchangeClient = scope.ServiceProvider.GetRequiredService<ICryptoProClientService>();

                foreach (var (userId, settings) in _stateRepo.ActiveBots)
                {
                    if (!settings.IsRunning)
                        continue;

                    var currency = settings.CurrencyPair;
                    var currentPrice = await exchangeClient.GetCurrencyPriceAsync(currency, stoppingToken);
                    if (currentPrice >= settings.UpperPrice ||
                        currentPrice <= settings.BottomPrice)
                    {
                        await exchangeClient.CreateSellOrderAsync(currency, settings.Amount, stoppingToken);
                        _stateRepo.ActiveBots.TryRemove(userId, out _);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception e)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}