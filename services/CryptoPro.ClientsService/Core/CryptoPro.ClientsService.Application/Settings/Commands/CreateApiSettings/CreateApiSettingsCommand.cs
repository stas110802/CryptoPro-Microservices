using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;

namespace CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;

public sealed record CreateApiSettingsCommand(ExchangeType Exchange, ApiSettingsCreateDto Settings, int UserId)
    : IRequest<bool>;