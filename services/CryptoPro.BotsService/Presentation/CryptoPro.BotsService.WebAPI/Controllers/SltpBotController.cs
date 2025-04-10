using System.IdentityModel.Tokens.Jwt;
using CryptoPro.BotsService.Application.Services;
using CryptoPro.BotsService.Domain.Dtos;
using CryptoPro.BotsService.Infrastructure.Redis;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.BotsService.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/bot/")]
public sealed class SltpBotController : ControllerBase
{
    private readonly IBotService _botService;
    private readonly IRedisService _cache;
    private readonly IConfiguration _config;
    private readonly IJwtParser _jwtParser;
    public SltpBotController(
        IBotService botService,
        IRedisService cache,
        IConfiguration config, 
        IJwtParser jwtParser)
    {
        _botService = botService;
        _cache = cache;
        _config = config;
        _jwtParser = jwtParser;
    }

    [HttpPost("run")]
    public async Task<ActionResult> RunBot([FromBody] SltpSettingsCreateDto settings)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var expiry = _jwtParser.GetRemainingLifetime(token) ?? TimeSpan.FromMinutes(5);
        
        await _cache.SetAsync($"jwt:{userId}", token, expiry);
        
        settings.UserId = userId.Value;
        var id = await _botService.StartBotAsync(settings);

        return Ok(new { Id = id });
    }

    [HttpPost("stop")]
    public async Task<ActionResult> StopBot(Guid botId)
    {
        var isStop = await _botService.StopBotAsync(botId);

        return Ok(new { Status = isStop });
    }

    private int? GetUserId()
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
        var isSuccessful = int.TryParse(userIdClaim, out var userId);

        return isSuccessful ? userId : null;
    }
}