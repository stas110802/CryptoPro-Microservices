using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Types;

namespace CryptoPro.ClientsService.Domain.Repositories;

public interface IApiSettingsRepository
{
    Task<IEnumerable<ApiSettingsEntity>> GetAllApiSettingsAsync();
    Task<ApiSettingsEntity?> GetApiSettingsByIdAsync(int id);
    Task<ApiSettingsEntity?> GetApiSettingsByUserIdAsync(ExchangeType type, int userId);
    Task<bool> AddApiSettingsAsync(ApiSettingsEntity settings);
    Task<bool> UpdateApiSettingsAsync(ApiSettingsEntity settings);
    Task<bool> DeleteApiSettingsAsync(int id);
}