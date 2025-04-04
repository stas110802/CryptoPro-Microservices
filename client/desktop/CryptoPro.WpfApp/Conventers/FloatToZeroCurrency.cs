using System.Globalization;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class FloatToZeroCurrency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var res = ((float)value).ToString("C0", CultureInfo.CurrentCulture);
        
        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}