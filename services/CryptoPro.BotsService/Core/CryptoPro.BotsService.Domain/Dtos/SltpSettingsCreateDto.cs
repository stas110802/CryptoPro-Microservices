using CryptoPro.BotsService.Domain.Types;

namespace CryptoPro.BotsService.Domain.Dtos;

public sealed class SltpSettingsCreateDto
{
    public ExchangeType Exchange { get; set; }
    public string CurrencyPair { get; set; }
    public decimal UpperPrice { get; set; }
    public decimal BottomPrice { get; set; }
    public decimal Amount { get; set; }
    public bool IsRunning { get; set; }
}