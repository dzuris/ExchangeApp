using System.Globalization;

namespace ExchangeApp.App.Converters;

public class IsNotDomesticCurrencyCode : IValueConverter
{
    private const string DomesticCurrencyCode = "EUR";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string currencyCode)
        {
            return true;
        }

        return currencyCode != DomesticCurrencyCode;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}