using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ExchangeClients.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class MarketController : Controller
{
    public MarketController()
    {
        
    }

    public IActionResult GetCurrencyInfo()
    {
        return Ok();
    }
}