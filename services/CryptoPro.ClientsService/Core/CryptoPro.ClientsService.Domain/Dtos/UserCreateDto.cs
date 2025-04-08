using System.ComponentModel.DataAnnotations;

namespace CryptoPro.ClientsService.Domain.Dtos;

public sealed class UserCreateDto
{
    [Required] 
    [StringLength(50)] 
    public string Username { get; set; }

    [Required] 
    [StringLength(50)] 
    public string Login { get; set; }

    [Required] 
    public string Password { get; set; }

    public string? AvatarPath { get; set; }
}