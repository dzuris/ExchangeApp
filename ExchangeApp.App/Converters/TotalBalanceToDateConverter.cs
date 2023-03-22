using System.Globalization;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class TotalBalanceToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TotalBalanceModel)
        {
            return value;
        }

        var model = (TotalBalanceModel)value;
        return value switch
        {
            TotalBalanceModel { Type: TotalBalanceType.Monthly } => model.Created.ToString("MMMM yyyy"),
            TotalBalanceModel { Type: TotalBalanceType.Annual } => model.Created.Year,
            _ => model.Created
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}