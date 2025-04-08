namespace CryptoPro.ClientsService.Domain.Clients.Models;

public class Order
{
    public string Currency { get; set; } = string.Empty;
    public decimal OrderId { get; set; }
}