using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("api/test/")]
public class PongController : ControllerBase
{
    [HttpGet("pin")]
    public ActionResult GetCurrencyPrice()
    {
        return Ok("pong");
    }
}