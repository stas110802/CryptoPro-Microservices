using CryptoPro.ClientsService.Application.Account.Queries.GetAccountBalance;
using CryptoPro.ClientsService.Application.Account.Queries.GetCurrencyAccountBalance;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("[controller]/{exchange}/")]
public sealed class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyBalance>>> GetAccountBalance(ExchangeType exchange)
    {
        var balances = await _mediator.Send(new GetAccountBalanceQuery(exchange));
        
        return Ok(balances);
    }

    [HttpGet("{currency}")]
    public async Task<ActionResult<CurrencyBalance>> GetCurrencyAccountBalance(ExchangeType exchange, string currency)
    {
        var balance = await _mediator.Send(new GetCurrencyAccountBalanceQuery(exchange, currency));
        
        return Ok(balance);
    }
}