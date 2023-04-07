using System.Globalization;
using System.Resources;

namespace ExchangeApp.App.Converters;

public class DecimalToDecimalWithSignStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not decimal)
        {
            return "Can not convert non decimal";
        }
        
        var decimalValue = (decimal)value;
        if (decimalValue < 0)
        {
            return decimalValue.ToString(CultureInfo.CurrentCulture);
        }

        return "+" + decimalValue.ToString(CultureInfo.CurrentCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}