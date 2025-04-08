using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;

public sealed record CreateApiSettingsCommand(ApiSettingsCreateDto Settings) : IRequest<bool>;
