using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TotalBalanceToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TotalBalanceType)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(EnumTotalBalanceResources));

        var res = rm.GetString(value.ToString() ?? string.Empty) ?? "Error in converting total balance to string.";

        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}