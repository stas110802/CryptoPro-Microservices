using AutoMapper;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;

public sealed class CreateApiSettingsCommandHandler: IRequestHandler<CreateApiSettingsCommand, bool>
{
    private readonly IApiSettingsRepository _repository;

    public CreateApiSettingsCommandHandler(IApiSettingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CreateApiSettingsCommand request, CancellationToken cancellationToken)
    {
        return await _repository.AddApiSettingsAsync(request.Settings);
    }
}