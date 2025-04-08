using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.ClientsService.Persistence.Repositories;

public sealed class ExchangeRepository : IExchangeRepository
{
    private readonly ClientsDbContext _dbContext;

    public ExchangeRepository(ClientsDbContext dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<IEnumerable<ExchangeEntity>> GetAllExchangesAsync()
    {
        return await _dbContext
            .Exchanges
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> AddExchangeAsync(ExchangeEntity exchange)
    {
        await _dbContext
            .Exchanges
            .AddAsync(exchange);
        
        return await _dbContext
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteExchangeAsync(int exchangeId)
    {
        var exchange = await _dbContext
            .Exchanges
            .FindAsync(exchangeId);

        if (exchange == null)
            return false;

        _dbContext.Exchanges.Remove(exchange);

        return await _dbContext.SaveChangesAsync() > 0;
    }
}