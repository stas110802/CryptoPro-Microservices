using CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;
using CryptoPro.ClientsService.Application.Account.Queries.GetCurrencyAccountBalance;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/account/{exchange}/")]
public sealed class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CurrencyBalance>>> GetAccountBalance(ExchangeType exchange)
    {
        var balances = await _mediator.Send(new GetAccountBalanceQuery(exchange));

        return Ok(balances);
    }

    [HttpPost("currencyBalance")]
    public async Task<ActionResult<CurrencyBalance>> GetCurrencyAccountBalance(ExchangeType exchange,
        [FromQuery] string currency)
    {
        var balance = await _mediator.Send(new GetCurrencyAccountBalanceQuery(exchange, currency));

        return Ok(balance);
    }
}