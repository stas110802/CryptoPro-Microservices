namespace CryptoPro.BotsService.Application.Services;

public interface IJwtParser
{
    TimeSpan? GetRemainingLifetime(string token);
}