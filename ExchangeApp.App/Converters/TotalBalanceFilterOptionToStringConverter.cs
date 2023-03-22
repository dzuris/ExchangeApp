using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TotalBalanceFilterOptionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TotalBalanceFilterOption)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(EnumTotalBalanceFilterOption));

        var res = rm.GetString(value.ToString() ?? string.Empty) ??
                  "Error in converting total balance filter option to string.";

        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}