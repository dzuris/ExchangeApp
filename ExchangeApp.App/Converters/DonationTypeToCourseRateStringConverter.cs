using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class DonationTypeToCourseRateStringConverter : IValueConverter
{
    private const string ErrorString = "Error in getting course rate label";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var rm = new ResourceManager(typeof(DonationPageResources));

        if (value is not DonationType)
        {
            return rm.GetString("CourseRateLabel") ?? ErrorString;
        }

        return value switch
        {
            DonationType.Deposit => rm.GetString("CourseRateDepositLabel") ?? ErrorString,
            DonationType.Withdraw => rm.GetString("CourseRateWithdrawLabel") ?? ErrorString,
            DonationType.Levy => rm.GetString("CourseRateLevyLabel") ?? ErrorString,
            _ => ErrorString
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}