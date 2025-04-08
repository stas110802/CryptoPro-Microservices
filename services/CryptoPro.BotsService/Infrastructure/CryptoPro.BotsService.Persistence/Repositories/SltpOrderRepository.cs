using CryptoPro.BotsService.Domain.Entities;
using CryptoPro.BotsService.Persistence.Data;
using CryptoPro.BotsService.Domain.Dtos;
using CryptoPro.BotsService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.BotsService.Persistence.Repositories;

public sealed class SltpOrderRepository : ISltpOrderRepository
{
    private readonly BotsDbContext _dbContext;

    public SltpOrderRepository(BotsDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<bool> AddOrder(SltpOrderEntity order)
    {
        await _dbContext.AddAsync(order);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<SltpOrderEntity>> GetUserOrders(int userId)
    {
        return await _dbContext
            .SltpOrders
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}