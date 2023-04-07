using System.Globalization;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TotalBalanceToColorConverter : IValueConverter
{
    public Color DailyColor { get; set; } = null!;
    public Color DailyColorDark { get; set; } = null!;
    public Color MonthlyColor { get; set; } = null!;
    public Color MonthlyColorDark { get; set; } = null!;

    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (Application.Current?.RequestedTheme is AppTheme.Light)
        {
            return value switch
            {
                TotalBalanceType.Monthly => MonthlyColor,
                _ => DailyColor
            };
        }

        return value switch
        {
            TotalBalanceType.Monthly => MonthlyColorDark,
            _ => DailyColorDark
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}