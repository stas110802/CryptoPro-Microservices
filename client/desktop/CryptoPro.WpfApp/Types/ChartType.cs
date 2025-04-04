namespace CryptoPro.WpfApp.Types;

public class ChartType : BaseType<string>
{
    private ChartType(string value) : base(value) { }

    public static ChartType PriceChart { get; } = new("Prices");
    public static ChartType CandlestickChart { get; } = new("Candlesticks");
}