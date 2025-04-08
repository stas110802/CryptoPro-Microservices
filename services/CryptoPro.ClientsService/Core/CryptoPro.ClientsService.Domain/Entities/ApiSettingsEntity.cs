namespace CryptoPro.ClientsService.Domain.Entities;

public sealed class ApiSettingsEntity
{
    public int Id { get; set; }
    public string? PublicKey { get; set; }
    public string? SecretKey { get; set; }
    public string? SpecificSettings { get; set; }
    
    public int UserId { get; set; }
    public UserEntity? User { get; set; }
    
    public int ExchangeId { get; set; }
    public ExchangeEntity? Exchange { get; set; }
}