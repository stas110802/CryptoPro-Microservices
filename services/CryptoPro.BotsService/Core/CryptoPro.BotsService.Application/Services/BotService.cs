using System.Collections.Concurrent;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Dtos;
using CryptoPro.BotsService.Domain.Entities;

namespace CryptoPro.BotsService.Application.Services;

public sealed class BotService : IBotService
{
    private readonly ConcurrentDictionary<Guid, BotWorker> _activeBots;
    private readonly ICryptoProClientService _exchangeClient;

    public BotService(ICryptoProClientService exchangeClient)
    {
        _exchangeClient = exchangeClient;
        _activeBots = new ConcurrentDictionary<Guid, BotWorker>();
    }

    public async Task<Guid> RunSltpBotAsync(SltpSettingsCreateDto settings)
    {
        var botId = Guid.NewGuid();
        var bot = new BotWorker(
            _exchangeClient,
            settings.CurrencyPair,
            settings.UpperPrice,
            settings.BottomPrice,
            settings.Amount);

        var isSuccessful = _activeBots.TryAdd(botId, bot);
        if (isSuccessful is false)
            throw new Exception($"Failed to add bot {botId}");
        
        await Task.Delay(1000);
        //await bot.StartAsync();

        return botId;
    }

    public bool StopSltpBot(Guid botId)
    {
        if (!_activeBots.TryGetValue(botId, out var bot))
            return false;

        bot.Stop();
        _activeBots.TryRemove(botId, out _);

        return true;
    }
}