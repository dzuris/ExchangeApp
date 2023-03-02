using System.Globalization;

namespace ExchangeApp.App.Utilities;

public class Utilities
{
    /// <summary>
    /// Converts string to decimal for both 3.14 and 3,14 formats (dots, comma)
    /// </summary>
    /// <param name="str">Decimal as string</param>
    /// <returns>Converted decimal number or null if string is not valid</returns>
    public static decimal? StrToDecimal(string str)
    {
        if (decimal.TryParse(str, CultureInfo.CurrentCulture, out var result))
        {
            return result;
        }

        if (decimal.TryParse(str, CultureInfo.InvariantCulture, out result))
        {
            return result;
        }

        return null;
    }
}