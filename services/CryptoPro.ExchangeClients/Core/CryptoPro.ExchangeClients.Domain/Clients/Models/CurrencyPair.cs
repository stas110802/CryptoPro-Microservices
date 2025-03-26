namespace CryptoPro.ExchangeClients.Domain.Clients.Models;

public class CurrencyPair
{
    public string Currency { get; set; } = string.Empty;
    public decimal SellingPrice { get; set; }
    public decimal TradingVolume { get; set; }
}