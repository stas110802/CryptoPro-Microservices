using CryptoPro.BotsService.Domain.Dtos;
using CryptoPro.BotsService.Domain.Entities;

namespace CryptoPro.BotsService.Domain.Repositories;

public interface ISltpOrderRepository
{
    Task<bool> AddOrder(SltpOrderEntity order);
    Task<IEnumerable<SltpOrderEntity>> GetUserOrders(int userId);
}