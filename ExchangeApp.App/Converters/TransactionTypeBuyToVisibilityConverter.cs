using System.Globalization;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TransactionTypeBuyToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TransactionType type)
        {
            return false;
        }

        return type == TransactionType.Buy;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}