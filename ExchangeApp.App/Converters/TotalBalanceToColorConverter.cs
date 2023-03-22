using System.Globalization;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TotalBalanceToColorConverter : IValueConverter
{
    public Color DailyColor { get; set; }
    public Color MonthlyColor { get; set; }
    public Color AnnualColor { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            TotalBalanceType.Monthly => MonthlyColor,
            TotalBalanceType.Annual => AnnualColor,
            _ => DailyColor
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}