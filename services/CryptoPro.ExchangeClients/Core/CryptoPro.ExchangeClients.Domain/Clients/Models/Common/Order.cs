namespace CryptoPro.ExchangeClients.Domain.Clients.Models.Common;

public class Order
{
    public string Currency { get; set; } = string.Empty;
    public decimal OrderId { get; set; }
}