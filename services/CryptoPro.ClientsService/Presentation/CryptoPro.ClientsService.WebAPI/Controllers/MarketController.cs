using CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyInformation;
using CryptoPro.ClientsService.Application.Market.Queries.GetCurrencyPrice;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
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
    
    [HttpGet("getPrice/{currency}")]
    public async Task<ActionResult<decimal>> GetCurrencyPrice(ExchangeType exchange,string currency)
    {
        var price = await _mediator.Send(new GetCurrencyPriceQuery(exchange, currency));

        return Ok(new { Price = price });
    }
    
    [HttpGet("getInfo/{currency}")]
    public async Task<ActionResult<CurrencyPair>> GetCurrencyInformation(ExchangeType exchange,string currency)
    {
        var currencyPair = await _mediator.Send(new GetCurrencyInformationQuery(exchange, currency));

        return Ok(currencyPair);
    }
}