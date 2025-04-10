﻿using CryptoPro.BotsService.Domain.Dtos;

namespace CryptoPro.BotsService.Application.Services;

public interface IBotService
{
    Task<Guid> StartBotAsync(SltpSettingsCreateDto settings);
    Task<bool> StopBotAsync(Guid botId);
}