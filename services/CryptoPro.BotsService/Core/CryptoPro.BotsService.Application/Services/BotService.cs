using CryptoPro.BotsService.Application.Repositories;
using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Services;

public class BotService : IBotService
{
    private readonly IBotStateRepository _stateRepo;

    public BotService(IBotStateRepository stateRepo)
    {
        _stateRepo = stateRepo;
    }

    public async Task<Guid> StartBotAsync(SltpSettingsCreateDto settings)
    {
        var botId = Guid.NewGuid();
        settings.IsRunning = true;
        _stateRepo.ActiveBots.TryAdd(botId, settings);
        await Task.CompletedTask;
        
        return botId;
    }

    public async Task<bool> StopBotAsync(Guid userId)
    {
        var isSuccessful = _stateRepo.ActiveBots.TryRemove(userId, out _);
        await Task.Delay(1);
        
        return isSuccessful;
    }
}