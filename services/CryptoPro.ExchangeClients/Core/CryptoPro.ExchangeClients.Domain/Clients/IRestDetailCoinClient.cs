using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;

namespace CryptoPro.ExchangeClients.Domain.Clients;

public interface IRestDetailCoinClient
{
    Task<IEnumerable<CoinInformation>> GetDetailCoinsInformationAsync(string secondCurrency, int page = 1,
        int perPage = 250, bool sparkline = true);

    Task<CoinFullDataById> GetDetailCoinInformationById(string id);

    Task<MarketChart> GetMarketChartRangeByCoinId(string id, string vsCurrency, string from, string to);
}