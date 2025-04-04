using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class CurrencyToVisibility : IMultiValueConverter
{ 
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not string selectedCurrency ||
            values[1] is not string parameterCurrency
           ) return Visibility.Collapsed;

        return selectedCurrency == parameterCurrency ? Visibility.Visible : Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}