namespace CryptoPro.WpfApp;

public class BaseType<T>(T value)
    where T : class
{
    public T Value { get; } = value;
}