using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoPro.BotsService.Application.Services.Backgrounds;

public class BotBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    

    public BotBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var botManager = scope.ServiceProvider.GetRequiredService<IBotManager>();
                    await botManager.CheckPricesAndExecuteTradesAsync(stoppingToken);
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}