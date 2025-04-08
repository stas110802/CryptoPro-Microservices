using CryptoPro.CoinsService.Domain.Models.CoinGecko;

namespace CryptoPro.CoinsService.Domain.Interfaces;

public interface IRestDetailCoinClient
{
    Task<IEnumerable<CoinBasicInformation>> GetDetailCoinsInformationAsync(string secondCurrency, int page = 1,
        int perPage = 250, bool sparkline = true);

    Task<CoinDetailedInformation> GetDetailCoinInformationById(string id);

    Task<MarketChart> GetMarketChartRangeByCoinId(string id, string vsCurrency, string from, string to);
}