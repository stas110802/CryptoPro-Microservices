using System.Collections.ObjectModel;
using CryptoPro.WpfApp.MVVM.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoPro.WpfApp;

public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ObservableCollection<Coin>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var items = new ObservableCollection<Coin>();
        int id = 1;

        if (reader.TokenType == JsonToken.StartArray)
        {
            reader.Read();

            while (reader.TokenType != JsonToken.EndArray)
            {
                var item = serializer.Deserialize<Coin>(reader);
                if (item != null)
                {
                    item.Id = id;
                    id++;
                    items.Add(item);
                }

                reader.Read();
            }
        }

        return items;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
    }
}