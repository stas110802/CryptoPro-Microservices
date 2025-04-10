using CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyInformation;
using CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyPrice;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.Common.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("api/market/{exchange}/")]
public sealed class MarketController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getPrice")]
    public async Task<ActionResult<decimal>> GetCurrencyPrice(ExchangeType exchange,
        [FromQuery] string currency)
    {
        var userId = JwtHelper.GetUserId(User) ?? throw new UnauthorizedAccessException();
        var price = await _mediator.Send(new GetCurrencyPriceQuery(exchange, currency, userId));

        return Ok(new { Price = price });
    }

    [HttpGet("getInfo")]
    public async Task<ActionResult<CurrencyPair>> GetCurrencyInformation(ExchangeType exchange,
        [FromQuery] string currency)
    {
        var userId = JwtHelper.GetUserId(User) ?? throw new UnauthorizedAccessException();
        var currencyPair = await _mediator.Send(new GetCurrencyInformationQuery(exchange, currency, userId));

        return Ok(currencyPair);
    }
}