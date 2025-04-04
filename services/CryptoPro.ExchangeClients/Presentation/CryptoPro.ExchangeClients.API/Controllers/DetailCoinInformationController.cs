using CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinInformationById;
using CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinsInformation;
using CryptoPro.ExchangeClients.Application.Coins.Queries.GetMarketChartRangeInformation;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ExchangeClients.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DetailCoinInformationController : Controller
{
    private readonly IMediator _mediator;

    public DetailCoinInformationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{vsCurrency}&{page:int}")]
    public async Task<ActionResult<IEnumerable<CoinInformation>>> GetDetailCoinsInformation(string vsCurrency, int page)
    {
        var response = await _mediator.Send(new GetDetailCoinsInformationQuery(vsCurrency, page));

        return Ok(response);
    }

    [HttpGet("mchart/{id}&{vsCurrency}&{from}&{to}")]
    public async Task<ActionResult<MarketChart>> GetMarketChartRangeInformation(string id, string vsCurrency,
        string from, string to)
    {
        var response = await _mediator.Send(new GetMarketChartRangeInformationQuery(id, vsCurrency, from, to));

        return Ok(response);
    }
    
    [HttpGet("cfull/{id}")]
    public async Task<ActionResult<CoinFullDataById>> GetDetailCoinInformation(string id)
    {
        var response = await _mediator.Send(new GetDetailCoinInformationByIdQuery(id));

        return Ok(response);
    }
}