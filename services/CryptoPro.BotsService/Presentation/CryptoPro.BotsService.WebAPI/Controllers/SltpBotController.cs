using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.BotsService.WebAPI.Controllers;

public sealed class SltpBotController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SltpBotController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
}