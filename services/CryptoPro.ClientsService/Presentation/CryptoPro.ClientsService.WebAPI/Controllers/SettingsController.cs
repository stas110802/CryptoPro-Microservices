using CryptoPro.ClientsService.Application.Settings.Commands.CreateApiSettings;
using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.Common.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/settings/{exchange}/")]
public sealed class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateApiSettings(ExchangeType exchange,
        [FromBody] ApiSettingsCreateDto settingsCreateDto)
    {
        var userId = JwtHelper.GetUserId(User) ?? throw new UnauthorizedAccessException();
        var isSuccessful = await _mediator.Send(new CreateApiSettingsCommand(exchange, settingsCreateDto, userId));

        return Ok(new { Status = isSuccessful });
    }
}