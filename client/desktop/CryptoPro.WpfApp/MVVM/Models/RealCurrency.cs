namespace CryptoPro.WpfApp.MVVM.Models;

public class RealCurrency
{
    public RealCurrency(string name, string symbol, string id)
    {
        Name = name;
        Symbol = symbol;
        Id = id;
    }
    
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
}