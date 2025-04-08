using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;

namespace CryptoPro.ClientsService.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity?> GetUserByIdAsync(int userId);
    Task<bool> AddUserAsync(UserCreateDto user);
    Task<bool> UpdateUserAsync(UserUpdateDto user);
    Task<bool> DeleteUserAsync(int userId);
}