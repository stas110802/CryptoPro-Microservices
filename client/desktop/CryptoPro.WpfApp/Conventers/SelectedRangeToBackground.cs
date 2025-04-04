using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CryptoPro.WpfApp.MVVM.Models;

namespace CryptoPro.WpfApp.Conventers;

public class SelectedRangeToBackground : IMultiValueConverter
{
    readonly BrushConverter brushConverter = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is TimeRange parameterRange &&
            values[1] is TimeRange selectedRange)
        {
            var bg = "#161616";
            var selectedBg = "#2c2c2c";

            var color = parameterRange.Range == selectedRange.Range ? selectedBg : bg;

            return (SolidColorBrush)brushConverter.ConvertFrom(color) ?? Brushes.White;
        }
        
        if (values[0] is BaseType<string> parameterType &&
            values[1] is BaseType<string> selectedType)
        {
            var bg = "#161616";
            var selectedBg = "#2c2c2c";

            var color = parameterType.Value == selectedType.Value ? selectedBg : bg;

            return (SolidColorBrush)brushConverter.ConvertFrom(color) ?? Brushes.White;
        }
        
        return Brushes.White;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}