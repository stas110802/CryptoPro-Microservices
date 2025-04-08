namespace CryptoPro.ClientsService.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Login { get; set; }
    public string HashPassword { get; set; }
    public string? AvatarPath { get; set; }

    public List<ApiSettingsEntity> ApiSettings { get; set; } = [];
}