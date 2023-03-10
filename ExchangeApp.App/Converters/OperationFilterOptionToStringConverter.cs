using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;
using System.Globalization;
using System.Resources;

namespace ExchangeApp.App.Converters;

public class OperationFilterOptionToStringConverter : IValueConverter
{
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not OperationFilterOption)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(EnumOperationFilterOptionsResources));

        var res = rm.GetString(value.ToString() ?? string.Empty) ?? "Error in converting operation filter option to the text resources";

        return res;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
