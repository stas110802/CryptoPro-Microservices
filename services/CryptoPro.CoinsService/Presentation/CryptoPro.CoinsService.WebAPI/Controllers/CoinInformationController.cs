using CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinInformationById;
using CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinsInformation;
using CryptoPro.CoinsService.Application.Coins.Queries.GetMarketChartRangeInformation;
using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.CoinsService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CoinInformationController : Controller
{
    private readonly IMediator _mediator;

    public CoinInformationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{vsCurrency}&{page:int}")]
    public async Task<ActionResult<IEnumerable<CoinBasicInformation>>> GetDetailCoinsInformation(string vsCurrency, int page)
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
    public async Task<ActionResult<CoinDetailedInformation>> GetDetailCoinInformation(string id)
    {
        var response = await _mediator.Send(new GetDetailCoinInformationByIdQuery(id));

        return Ok(response);
    }
}