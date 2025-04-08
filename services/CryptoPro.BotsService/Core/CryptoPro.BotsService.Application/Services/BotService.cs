using System.Collections.Concurrent;
using CryptoPro.BotsService.Domain;
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

    public Guid RunSltpBot(string currencyPair,
        decimal sellPrice,
        decimal upperPrice,
        decimal bottomPrice,
        decimal amount)
    {
        var botId = Guid.NewGuid();
        var bot = new BotWorker(
            _exchangeClient,
            currencyPair,
            sellPrice,
            upperPrice,
            bottomPrice,
            amount);

        var isSuccessful = _activeBots.TryAdd(botId, bot);
        if(isSuccessful is false)
            throw new Exception($"Failed to add bot {botId}");
        
        bot.Start();
        
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