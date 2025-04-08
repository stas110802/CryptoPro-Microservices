using CryptoPro.ClientsService.Application.Trade.Commands.CancelAllOrders;
using CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;
using CryptoPro.ClientsService.Application.Trade.Queries.GetAccountOrders;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
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

    [HttpPost("getAllOrders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAccountOrders(ExchangeType exchange)
    {
        var orders = await _mediator.Send(new GetAccountOrdersQuery(exchange));
        
        return Ok(orders);
    }
    
    [HttpPost("cancelAllOrders")]
    public async Task<ActionResult<bool>> CancelAllAccountOrders(ExchangeType exchange)
    {
        var isSuccessful = await _mediator.Send(new CancelAllOrdersCommand(exchange));
        
        return Ok(new { Status = isSuccessful });
    }
    
    [HttpPost("sellOrder")]
    public async Task<ActionResult<bool>> CreateSellOrder(ExchangeType exchange, string currency, decimal amount, decimal price)
    {
        var isSuccessful = await _mediator.Send(new CreateSellOrderCommand(exchange, currency, amount, price));
        
        return Ok(new { Status = isSuccessful });
    }
}