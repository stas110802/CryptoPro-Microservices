using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.ClientsService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.ClientsService.Persistence.Repositories;

public sealed class ApiSettingsRepository : IApiSettingsRepository
{
    private readonly ClientsDbContext _dbContext;

    public ApiSettingsRepository(ClientsDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<IEnumerable<ApiSettingsEntity>> GetAllApiSettingsAsync()
    {
        return await _dbContext
            .ApiSettings
            .AsNoTracking()
            .Include(s => s.User)
            .Include(s => s.Exchange)
            .ToListAsync();
    }

    public async Task<ApiSettingsEntity?> GetApiSettingsByIdAsync(int id)
    {
        var apiSettings = await _dbContext
            .ApiSettings
            .FindAsync(id);

        return apiSettings;
    }

    public async Task<ApiSettingsEntity?> GetApiSettingsByUserIdAsync(ExchangeType type, int userId)
    {
        return await _dbContext
            .ApiSettings
            .AsNoTracking()
            .Include(s => s.Exchange)
            .FirstOrDefaultAsync(s 
                => s.UserId == userId && 
                   s.Exchange != null && 
                   s.Exchange.Type == type);
    }

    public async Task<bool> AddApiSettingsAsync(ApiSettingsEntity settings)
    {
        await _dbContext
            .ApiSettings
            .AddAsync(settings);

        return await _dbContext
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateApiSettingsAsync(ApiSettingsEntity settings)
    {
        _dbContext
            .ApiSettings
            .Update(settings);

        return await _dbContext
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteApiSettingsAsync(int id)
    {
        var settings = await _dbContext
            .ApiSettings
            .FindAsync(id);

        if (settings == null)
            return false;

        _dbContext.ApiSettings.Remove(settings);

        return await _dbContext.SaveChangesAsync() > 0;
    }
}