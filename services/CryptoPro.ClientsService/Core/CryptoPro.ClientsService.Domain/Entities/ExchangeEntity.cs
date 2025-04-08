using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Domain.Entities;

public sealed class ExchangeEntity
{
    public int Id { get; set; }
    public required ExchangeType Type { get; set; }
    
    public List<ApiSettingsEntity> ApiSettings { get; set; } = [];
}