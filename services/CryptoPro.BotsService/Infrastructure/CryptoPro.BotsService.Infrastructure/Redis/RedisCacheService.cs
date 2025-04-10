using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace CryptoPro.BotsService.Infrastructure.Redis;

public sealed class RedisCacheService : IRedisService
{
    private readonly IDatabase _redisDb;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
    }

    public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
    {
        await _redisDb.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetAsync(string key)
    {
        return await _redisDb.StringGetAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        await _redisDb.StringGetDeleteAsync(key);
    }
}