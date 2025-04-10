using AutoMapper;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;

public sealed class CreateApiSettingsCommandHandler : IRequestHandler<CreateApiSettingsCommand, bool>
{
    private readonly IApiSettingsRepository _apiSettingsRepository;
    private readonly IExchangeRepository _exchangeRepository;

    public CreateApiSettingsCommandHandler(
        IApiSettingsRepository apiSettingsRepository,
        IExchangeRepository exchangeRepository)
    {
        _apiSettingsRepository = apiSettingsRepository;
        _exchangeRepository = exchangeRepository;
    }

    public async Task<bool> Handle(CreateApiSettingsCommand request, CancellationToken cancellationToken)
    {
        var exchangeId = await _exchangeRepository.GetExchangeIdByTypeAsync(request.Exchange);
        var settings = new ApiSettingsEntity
        {
            PublicKey = request.Settings.PublicKey,
            SecretKey = request.Settings.SecretKey,
            SpecificSettings = request.Settings.SpecificSettings,
            UserId = request.UserId,
            ExchangeId = exchangeId
        };

        return await _apiSettingsRepository.AddApiSettingsAsync(settings);
    }
}