using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.BL.Models.Customer;

namespace ExchangeApp.App.Converters;

public class CustomerDetailModelToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not CustomerDetailModel)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(CustomerResources));

        return value switch
        {
            IndividualCustomerDetailModel => rm.GetString("IndividualCustomer") ??
                                             "Error in converting individual customer to string",
            BusinessCustomerDetailModel => rm.GetString("BusinessCustomer") ??
                                           "Error in converting business customer to string",
            MinorCustomerDetailModel => rm.GetString("MinorCustomer") ?? "Error in converting minor customer to string",
            CustomerDetailModel => "Customer detail model not set yet",
            _ => $"Unsupported customer detail type: {value.GetType().Name}"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}