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

    /// <summary>
    /// Function returns date time from slovak identification number
    /// </summary>
    /// <param name="number">String number of 6 length (2 chars for year, 2 for month and 2 for day)</param>
    /// <returns>DateTime if given number is valid</returns>
    public static DateTime? GetDateTimeFromIdentificationNumber(string number)
    {
        if (number.Length < 6) return null;
        number = number.Substring(0, 6);

        var year = number.Substring(0, 2);
        var month = number.Substring(2, 2);
        var day = number.Substring(4, 2);

        if (int.TryParse(year, out var yearInt))
        {
            if (yearInt + 2000 < DateTime.Now.Year)
            {
                yearInt += 2000;
            }
            else
            {
                yearInt += 1900;
            }
        }
        else
        {
            return null;
        }

        if (int.TryParse(month, out var monthInt))
        {
            switch (monthInt)
            {
                case >= 71 and <= 82:
                    monthInt -= 70;
                    break;
                case >= 51 and <= 62:
                    monthInt -= 50;
                    break;
                case >= 21 and <= 32:
                    monthInt -= 20;
                    break;
                case >= 1 and <= 12:
                    break;
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }

        if (!int.TryParse(day, out var dayInt))
        {
            return null;
        }

        try
        {
            return new DateTime(yearInt, monthInt, dayInt);
        }
        catch
        {
            return null;
        }
    }
}