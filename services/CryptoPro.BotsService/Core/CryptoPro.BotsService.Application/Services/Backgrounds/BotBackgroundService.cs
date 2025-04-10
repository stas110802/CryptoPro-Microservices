using CryptoPro.BotsService.Application.Repositories;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Entities;
using CryptoPro.BotsService.Domain.Repositories;
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
                
                var orderRepository = scope.ServiceProvider.GetRequiredService<ISltpOrderRepository>();

                foreach (var (botId, settings) in _stateRepo.ActiveBots)
                {
                    if (!settings.IsRunning)
                        continue;
                    
                    var currency = settings.CurrencyPair;
                    var currentPrice = await exchangeClient.GetCurrencyPriceAsync(currency, stoppingToken);
                    if (currentPrice >= settings.UpperPrice ||
                        currentPrice <= settings.BottomPrice)
                    {
                        exchangeClient.SetUserId(settings.UserId);
                        exchangeClient.SetExchange(settings.Exchange);
                        
                        var isOrderCreated = await exchangeClient
                            .CreateSellOrderAsync(currency, settings.Amount, stoppingToken);
                        var isBotRemoved = _stateRepo
                            .ActiveBots
                            .TryRemove(botId, out _);

                        var exchangeId = await exchangeClient.GetExchangeIdByType(settings.Exchange, stoppingToken);
                        var orderEntity = new SltpOrderEntity
                        {
                            Currency = currency,
                            Amount = settings.Amount,
                            SellPrice = currentPrice,
                            UpperPrice = settings.UpperPrice,
                            BottomPrice = settings.BottomPrice,
                            ExchangeId = exchangeId,
                            UserId = settings.UserId
                        };
                        await orderRepository.AddOrder(orderEntity);
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