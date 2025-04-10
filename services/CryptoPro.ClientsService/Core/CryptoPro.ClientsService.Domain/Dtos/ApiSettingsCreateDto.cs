using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoPro.ClientsService.Domain.Dtos;

public sealed class ApiSettingsCreateDto
{
    public string? PublicKey { get; set; }
    public string? SecretKey { get; set; }
    public string? SpecificSettings { get; set; }
}