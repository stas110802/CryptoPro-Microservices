using System.Collections.Concurrent;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Services;

public class BotManager : IBotManager
{
    private readonly ConcurrentDictionary<Guid, SltpSettingsCreateDto> _activeBots = new();
    private readonly ICryptoProClientService _exchangeClient;

    public BotManager(ICryptoProClientService exchangeClient)
    {
        _exchangeClient = exchangeClient;
    }

    public async Task<Guid> StartBotAsync(SltpSettingsCreateDto settings)
    {
        var botId = Guid.NewGuid();
        settings.IsRunning = true;
        _activeBots.TryAdd(botId, settings);
        await Task.Delay(5);

        return botId;
    }

    public async Task<bool> StopBotAsync(Guid botId)
    {
        var isSuccessful = _activeBots.TryRemove(botId, out _);
        await Task.CompletedTask;

        return isSuccessful;
    }

    public async Task CheckPricesAndExecuteTradesAsync(CancellationToken cancellationToken)
    {
        foreach (var (userId, settings) in _activeBots)
        {
            if (!settings.IsRunning)
                continue;

            var currentPrice = await _exchangeClient.GetCurrencyPriceAsync("BTCUSDT", cancellationToken);
            if (currentPrice >= settings.UpperPrice || currentPrice <= settings.BottomPrice)
            {
                await _exchangeClient.CreateSellOrderAsync("BTCUSDT", settings.Amount, cancellationToken);
                _activeBots.TryRemove(userId, out _);
            }
        }
    }
}