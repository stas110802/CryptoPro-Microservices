using System.Collections.ObjectModel;
using CryptoPro.WpfApp.MVVM.Models;
using Newtonsoft.Json;

namespace CryptoPro.WpfApp;

public static class JsonExtensions
{
    public static ObservableCollection<Coin> FromJsonWithAutoIncrementId(this string json) 
    {
        var result = JsonConvert.DeserializeObject<ObservableCollection<Coin>>(json, new ItemConverter());
        if (result is null)
            throw new NullReferenceException("[ExchangeClients.JsonExtensions, method=FromJson] result is null");

        return result;
    }
}