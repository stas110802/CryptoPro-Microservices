using CryptoPro.ClientsService.Application.Exchanges.Queries.GetExchangeIdByName;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("api/exchanges/")]
public sealed class ExchangeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExchangeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getIdByName/{name}")]
    public async Task<ActionResult> GetExchangeIdByName(ExchangeType name)
    {
        var id = await _mediator.Send(new GetExchangeIdByNameQuery(name));

        return Ok(new { Id = id });
    }
}