using System.Globalization;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class PercentageToText : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return string.Empty;

        var type = value.GetType();
        dynamic percentage;

        if (type == typeof(int))
        {
            percentage = (int)value;
        }
        else if (type == typeof(double))
        {
            percentage = (double)value;
        }
        else if (type == typeof(float))
        {
            percentage = (float)value;
        }
        else
        {
            percentage = (decimal)value;
        }

        return (percentage >= 0 ? "⏶" : "⏷") +
               Math.Abs(percentage / 100)
                   .ToString("P", CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}