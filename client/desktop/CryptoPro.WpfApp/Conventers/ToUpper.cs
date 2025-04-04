using System.Globalization;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class ToUpper : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        var res = value?.ToString();
        
        return res == null ? string.Empty : res.ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}