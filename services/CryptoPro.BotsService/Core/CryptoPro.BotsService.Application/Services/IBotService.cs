using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Services;

public interface IBotService
{
    Task<Guid> RunSltpBotAsync(SltpSettingsCreateDto settings);
    bool StopSltpBot(Guid botId);
}