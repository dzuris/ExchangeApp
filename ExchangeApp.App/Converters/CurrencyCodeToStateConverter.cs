using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;

namespace ExchangeApp.App.Converters;

public class CurrencyCodeToStateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string)
        {
            return "Currency code to state converter error";
        }

        var rm = new ResourceManager(typeof(CurrencyResources));

        var result = rm.GetString((string)value) ?? "Unknown currency code converter error";

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}