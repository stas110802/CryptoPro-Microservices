using CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.Common.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/trade/{exchange}/")]
public sealed class TradeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TradeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sellOrder")]
    public async Task<ActionResult<bool>> CreateSellOrder(ExchangeType exchange,
        [FromQuery] string currency,
        [FromQuery] decimal amount,
        [FromQuery] decimal price)
    {
        var userId = JwtHelper.GetUserId(User) ?? throw new UnauthorizedAccessException();
        var isSuccessful = await _mediator.Send(new CreateSellOrderCommand(exchange, currency, amount, price, userId));

        return Ok(new { Status = isSuccessful });
    }
}