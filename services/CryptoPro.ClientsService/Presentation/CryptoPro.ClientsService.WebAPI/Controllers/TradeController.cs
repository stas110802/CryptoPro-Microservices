using CryptoPro.ClientsService.Application.Trade.Commands.CancelAllOrders;
using CryptoPro.ClientsService.Application.Trade.Commands.CreateSellOrder;
using CryptoPro.ClientsService.Application.Trade.Queries.GetAccountOrders;
using CryptoPro.ClientsService.Domain.Clients.Models;
using CryptoPro.ClientsService.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("[controller]/{exchange}/")]
public sealed class TradeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TradeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAccountOrders(ExchangeType exchange)
    {
        var orders = await _mediator.Send(new GetAccountOrdersQuery(exchange));
        
        return Ok(orders);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<bool>> CancelAllAccountOrders(ExchangeType exchange)
    {
        var isSuccessful = await _mediator.Send(new CancelAllOrdersCommand(exchange));
        
        return Ok(isSuccessful);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<bool>> CreateSellOrder(ExchangeType exchange, string currency, decimal amount, decimal price)
    {
        var isSuccessful = await _mediator.Send(new CreateSellOrderCommand(exchange, currency, amount, price));
        
        return Ok(isSuccessful);
    }
}