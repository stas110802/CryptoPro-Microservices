using CryptoPro.BotsService.Domain.Types;

namespace CryptoPro.BotsService.Domain.Entities;

public class SltpOrderEntity
{
    public int Id { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal SellPrice { get; set; }
    public decimal UpperPrice { get; set; }
    public decimal BottomPrice { get; set; }
    public SltpOrderType Type { get; set; }
    public int ExchangeId { get; set; }
    public int UserId { get; set; }
}