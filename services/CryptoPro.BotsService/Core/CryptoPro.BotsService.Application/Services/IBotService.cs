namespace CryptoPro.BotsService.Application.Services;

public interface IBotService
{
    Guid RunSltpBot(string currencyPair,
        decimal sellPrice,
        decimal upperPrice,
        decimal bottomPrice,
        decimal amount);
    bool StopSltpBot(Guid botId);
}