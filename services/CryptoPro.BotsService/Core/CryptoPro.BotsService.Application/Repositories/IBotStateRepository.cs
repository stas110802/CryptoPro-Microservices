using System.Collections.Concurrent;
using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Repositories;

public interface IBotStateRepository
{
    ConcurrentDictionary<Guid, SltpSettingsCreateDto> ActiveBots { get; }
}