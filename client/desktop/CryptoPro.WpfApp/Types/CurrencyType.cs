namespace CryptoPro.WpfApp.Types;

public sealed class CurrencyType : BaseType<string>
{
    private CurrencyType(string value) : base(value) { }

    public static CurrencyType Usd { get; } = new("usd");
    public static CurrencyType Eur { get; } = new("eur");
}