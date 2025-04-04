﻿using System.Globalization;
using System.Windows.Data;

namespace CryptoPro.WpfApp.Conventers;

public class DecimalToVariableCurrency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return string.Empty;

        var price = (decimal)value;

        int precision;
        
        if (price >= 1 || price < 0) 
            precision = 2;
        else if (price > 0.099M) 
            precision = 4;
        else 
            precision = (int)Math.Log10((double)(1M / price)) * 2;
        
        var res = string.Format(CultureInfo.CurrentCulture, "{0:C" + precision + "}", price);

        return res;// 0:N03
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}