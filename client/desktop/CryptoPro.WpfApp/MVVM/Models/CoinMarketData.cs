using Newtonsoft.Json;

namespace CryptoPro.WpfApp.MVVM.Models;

public class CoinMarketData
{
    public Dictionary<string, decimal> CurrentPrice { get; set; }
    public Dictionary<string, decimal> Ath { get; set; }
    public Dictionary<string, decimal> AthChangePercentage { get; set; }
    public Dictionary<string, DateTimeOffset> AthDate { get; set; }
    public Dictionary<string, decimal> Atl { get; set; }
    public Dictionary<string, decimal> AtlChangePercentage { get; set; }
    public Dictionary<string, DateTimeOffset> AtlDate { get; set; }
    public Dictionary<string, decimal> MarketCap { get; set; }
    public Dictionary<string, decimal> TotalVolume { get; set; }
    public Dictionary<string, decimal> High24H { get; set; }
    public Dictionary<string, decimal> Low24hH { get; set; }
    public Dictionary<string, decimal> PriceChangePercentage24HInCurrency { get; set; }
    public Dictionary<string, decimal> PriceChangePercentage7DInCurrency { get; set; }
    public Dictionary<string, decimal> PriceChangePercentage14DInCurrency { get; set; }
    public Dictionary<string, decimal> PriceChangePercentage30DInCurrency { get; set; }
    public Dictionary<string, decimal> PriceChangePercentage1YInCurrency { get; set; }
    public Dictionary<string, decimal> MarketCapChange24HInCurrency { get; set; }
    public Dictionary<string, decimal> MarketCapChangePercentage24HInCurrency { get; set; }
    public float PriceChangePercentage24H { get; set; }
    public float PriceChangePercentage7D { get; set; }
    public float PriceChangePercentage14D { get; set; }
    public float PriceChangePercentage30D { get; set; }
    public float PriceChangePercentage1Y { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonIgnore] public string Currency { get; set; } = "usd";

    public decimal CurrentCurrencyPrice => CurrentPrice[Currency];
    
    public decimal AthCurrency => Ath[Currency];
    public decimal AthChangePercentageCurrency => AthChangePercentage[Currency];
    public DateTimeOffset AthDateCurrency => AthDate[Currency];
    
    public decimal AtlCurrency => Atl[Currency];
    public decimal AtlChangePercentageCurrency => AtlChangePercentage[Currency];
    public DateTimeOffset AtlDateCurrency => AtlDate[Currency];
    
    public decimal CurrencyMarketCap => MarketCap[Currency];
    public decimal CurrencyTotalVolume => TotalVolume[Currency];
    
    public decimal CurrencyHigh24H => High24H[Currency];
    public decimal CurrencyLow24H => Low24hH[Currency];
    
    public decimal CurrencyPriceChangePercentage24HInCurrency => PriceChangePercentage24HInCurrency[Currency];
    public decimal CurrencyPriceChangePercentage7DInCurrency => PriceChangePercentage7DInCurrency[Currency];
    public decimal CurrencyPriceChangePercentage14DInCurrency => PriceChangePercentage14DInCurrency[Currency];
    public decimal CurrencyPriceChangePercentage30DInCurrency => PriceChangePercentage30DInCurrency[Currency];
    public decimal CurrencyPriceChangePercentage1YInCurrency => PriceChangePercentage1YInCurrency[Currency];
    
    public decimal CurrencyPriceChange24HInCurrency =>  CurrentCurrencyPrice / 100 * CurrencyPriceChangePercentage24HInCurrency;
    public decimal CurrencyPriceChange7DInCurrency => CurrentCurrencyPrice / 100 * CurrencyPriceChangePercentage7DInCurrency;
    public decimal CurrencyPriceChange14DInCurrency => CurrentCurrencyPrice / 100 * CurrencyPriceChangePercentage14DInCurrency;
    public decimal CurrencyPriceChange30DInCurrency => CurrentCurrencyPrice / 100 * CurrencyPriceChangePercentage30DInCurrency;
    public decimal CurrencyPriceChange1YInCurrency => CurrentCurrencyPrice / 100 * CurrencyPriceChangePercentage1YInCurrency;
    
    public decimal CurrencyMarketCapChange24HInCurrency => MarketCapChange24HInCurrency[Currency];
    public decimal CurrencyMarketCapChangePercentage24HInCurrency => MarketCapChangePercentage24HInCurrency[Currency];
}