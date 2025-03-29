using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

namespace CryptoPro.ExchangeClients.Domain.Clients;

public interface IRestDetailCoinClient
{
    Task<IEnumerable<DetailCoin>> GetDetailCoinsInfoAsync(string secondCurrency, int page = 1,
        int perPage = 250, bool sparkline = true);
}