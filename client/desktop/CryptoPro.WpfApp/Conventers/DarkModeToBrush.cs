using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class DarkModeToBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is string pipeDelimitedColors)
        {
            var colors = pipeDelimitedColors.Split('|');
            return DarkModeStore.Store.IsDarkMode ? colors[1] : colors[0];
        }
        return "#FCFF00";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class DarkModeStore : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private static readonly DarkModeStore _instance = new();
    private DarkModeStore() { }

    public static DarkModeStore Store
    {
        get
        {
            return _instance;
        }
    }

    public bool IsDarkMode { get; set; }
}