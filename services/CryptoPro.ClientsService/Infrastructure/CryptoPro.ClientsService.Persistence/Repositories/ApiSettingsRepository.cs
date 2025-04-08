using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
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

    public async Task<ApiSettingsEntity> GetApiSettingsByIdAsync(int id)
    {
        var apiSettings = await _dbContext
            .ApiSettings
            .FindAsync(id);
        if (apiSettings == null)
        {
            throw new KeyNotFoundException("Api-Settings does not exist");
        }

        return apiSettings;
    }

    public async Task<bool> AddApiSettingsAsync(ApiSettingsCreateDto settings)
    {
        var settingsEntity = new ApiSettingsEntity
        {
            PublicKey = settings.PublicKey,
            SecretKey = settings.SecretKey,
            SpecificSettings = settings.SpecificSettings,
            ExchangeId = settings.ExchangeId,
            UserId = settings.UserId
        };
        
        await _dbContext
            .ApiSettings
            .AddAsync(settingsEntity);

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