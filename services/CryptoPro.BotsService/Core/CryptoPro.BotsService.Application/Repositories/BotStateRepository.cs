using System.Collections.Concurrent;
using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Repositories;

public sealed class BotStateRepository : IBotStateRepository
{
    public ConcurrentDictionary<Guid, SltpSettingsCreateDto> ActiveBots { get; } = new();
}