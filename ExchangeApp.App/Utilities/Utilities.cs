using System.Globalization;

namespace ExchangeApp.App.Utilities;

public class Utilities
{
    /// <summary>
    /// Converts string to float for both 3.14 and 3,14 formats (dots, comma)
    /// </summary>
    /// <param name="str">Float as string</param>
    /// <returns>Converted float number or null if string is not valid</returns>
    public static float? StrToFloat(string str)
    {
        if (float.TryParse(str, CultureInfo.CurrentCulture, out var result))
        {
            return result;
        }

        if (float.TryParse(str, CultureInfo.InvariantCulture, out result))
        {
            return result;
        }

        return null;
    }
}