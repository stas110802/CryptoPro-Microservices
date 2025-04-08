using System.ComponentModel.DataAnnotations;

namespace CryptoPro.ClientsService.Domain.Dtos;

public sealed class ApiSettingsCreateDto
{
    public string? PublicKey { get; set; }
    public string? SecretKey { get; set; }
    public string? SpecificSettings { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ExchangeId { get; set; }
}