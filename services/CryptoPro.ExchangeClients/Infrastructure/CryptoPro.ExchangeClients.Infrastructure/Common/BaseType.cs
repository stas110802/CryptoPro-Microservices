namespace CryptoPro.ExchangeClients.Infrastructure.Common;

public abstract class BaseType(string value)
{
    public string Value { get; init; } = value;
}