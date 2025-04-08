using CryptoPro.BotsService.Application.Services;
using CryptoPro.BotsService.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.BotsService.WebAPI.Controllers;

[ApiController]
[Route("api/bot/")]
public sealed class SltpBotController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBotService _botService;

    public SltpBotController(IMediator mediator, IBotService botService)
    {
        _mediator = mediator;
        _botService = botService;
    }

    [HttpPost("run")]
    public async Task<ActionResult> RunBot([FromBody] SltpSettingsCreateDto settings)
    {
        var id = await _botService.StartBotAsync(settings);

        return Ok(new { Id = id });
    }

    [HttpPost("stop")]
    public async Task<ActionResult> StopBot(Guid botId)
    {
        var isStop = await _botService.StopBotAsync(botId);

        return Ok(new { Status = isStop });
    }
}