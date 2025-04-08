using CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;
using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> CreateApiSettings(ApiSettingsCreateDto settingsCreateDto)
    {
        var isSuccessful = await _mediator.Send(new CreateApiSettingsCommand(settingsCreateDto));
        
        return Ok(isSuccessful);
    }
}