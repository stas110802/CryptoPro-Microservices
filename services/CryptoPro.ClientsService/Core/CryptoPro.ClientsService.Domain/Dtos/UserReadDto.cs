namespace CryptoPro.ClientsService.Domain.Dtos;

public sealed class UserReadDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? AvatarPath { get; set; }
}