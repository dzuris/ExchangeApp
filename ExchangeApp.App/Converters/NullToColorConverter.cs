using System.Globalization;

namespace ExchangeApp.App.Converters;

public class NullToColorConverter : IValueConverter
{
    public Color NotNullColor { get; set; } = null!;
    public Color NotNullColorDark { get; set; } = null!;
    public Color NullColor { get; set; } = null!;
    public Color NullColorDark { get; set; } = null!;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (Application.Current?.RequestedTheme is AppTheme.Light)
        {
            return value is null ? NullColor : NotNullColor;
        }

        return value is null ? NullColorDark : NotNullColorDark;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}