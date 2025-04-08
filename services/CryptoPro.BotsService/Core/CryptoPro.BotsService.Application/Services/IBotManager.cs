using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Services;

public interface IBotManager
{
    Task<Guid> StartBotAsync(SltpSettingsCreateDto settings);
    Task<bool> StopBotAsync(Guid botId);
    Task CheckPricesAndExecuteTradesAsync(CancellationToken cancellationToken);
}