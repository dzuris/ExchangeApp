using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TransactionTypeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TransactionType)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(EnumTransactionTypeResources));

        var res = rm.GetString(value.ToString() ?? string.Empty) ?? "Error in converting transaction type to the text resources";

        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}