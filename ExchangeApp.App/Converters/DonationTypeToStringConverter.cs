﻿using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Converters;

public class DonationTypeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DonationType)
        {
            return value;
        }

        var rm = new ResourceManager(typeof(EnumDonationTypeResources));

        var res = rm.GetString("DonationType_" + value) ?? "Error in converting donation type to the text resources";

        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}