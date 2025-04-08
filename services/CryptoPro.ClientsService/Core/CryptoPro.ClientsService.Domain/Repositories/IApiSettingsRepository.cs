using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;

namespace CryptoPro.ClientsService.Domain.Repositories;

public interface IApiSettingsRepository
{
    Task<IEnumerable<ApiSettingsEntity>> GetAllApiSettingsAsync();
    Task<ApiSettingsEntity> GetApiSettingsByIdAsync(int id);
    Task<bool> AddApiSettingsAsync(ApiSettingsCreateDto settings);
    Task<bool> UpdateApiSettingsAsync(ApiSettingsEntity settings);
    Task<bool> DeleteApiSettingsAsync(int id);
}