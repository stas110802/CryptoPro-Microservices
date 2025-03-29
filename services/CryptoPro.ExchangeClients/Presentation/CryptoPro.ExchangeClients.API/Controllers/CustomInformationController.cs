using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using CryptoPro.ExchangeClients.Infrastructure.Clients.Rest.CoinGecko;
using CryptoPro.ExchangeClients.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoPro.ExchangeClients.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomInformationController : Controller
{
    private readonly IRestDetailCoinClient _detailCoinClient;

    public CustomInformationController(IRestDetailCoinClient detailCoinClient)
    {
        _detailCoinClient = detailCoinClient;
    }

    [HttpGet("{vsCurrency}")]
    public async Task<ActionResult> GetDetailCoinsInfo(string vsCurrency)
    {
        var result = await _detailCoinClient.GetDetailCoinsInfoAsync(vsCurrency);
        
        return Ok(result);
    }
}
