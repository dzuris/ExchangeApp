using System.Globalization;

namespace ExchangeApp.App.Converters;

public class NullToColorConverter : IValueConverter
{
    public Color NotNullColor { get; set; } = null!;
    public Color NullColor { get; set; } = null!;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null ? NullColor : NotNullColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}