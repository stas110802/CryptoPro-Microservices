using CryptoPro.ClientsService.Domain;
using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.ClientsService.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ClientsDbContext _dbContext;

    public UserRepository(ClientsDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .Include(u => u.ApiSettings)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        var user = await _dbContext
            .Users
            .FindAsync(userId);

        return user;
    }

    public async Task<bool> AddUserAsync(UserCreateDto user)
    {
        var userDb = new UserEntity
        {
            Username = user.Username,
            Login = user.Login,
            AvatarPath = user.AvatarPath,
            HashPassword = HashCalculator.CalculateHmacSha256Hash(user.Password, user.Login + user.Username)
        };

        await _dbContext
            .Users
            .AddAsync(userDb);

        return await _dbContext
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserAsync(UserUpdateDto user)
    {
        var userEntity = await _dbContext.Users.FindAsync(user.Id);
        if (userEntity == null)
            return false;

        userEntity.Username = user.Username ?? userEntity.Username;
        userEntity.AvatarPath = user.AvatarPath ?? userEntity.AvatarPath;

        return await _dbContext
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _dbContext
            .Users
            .FindAsync(userId);

        if (user == null)
            return false;

        _dbContext.Users.Remove(user);

        return await _dbContext
            .SaveChangesAsync() > 0;
    }
}