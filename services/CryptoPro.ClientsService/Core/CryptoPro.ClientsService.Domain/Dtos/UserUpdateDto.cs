using System.ComponentModel.DataAnnotations;

namespace CryptoPro.ClientsService.Domain.Dtos;

public sealed class UserUpdateDto
{
    [Required]
    public int Id { get; set; }
    
    [StringLength(50)] 
    public string? Username { get; set; }
    
    public string? AvatarPath { get; set; }
}