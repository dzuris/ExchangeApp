using System.Text.RegularExpressions;

namespace ExchangeApp.App.Utilities;

public class CustomValidators
{
    public static bool ValidateName(string name)
    {
        var nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
        return nameRegex.IsMatch(name);
    }

    public static bool ValidateIdentificationNumber(string identificationNumber)
    {
        var identificationNumberRegex =
            new Regex(
                @"^\s*[0-9]{2}(0[1-9]|[1-9][0-2])(0[1-9]|[1-2][0-9]|3[0-1])(\s*/\s*)?\d{1,4}\s*$");
        return identificationNumberRegex.IsMatch(identificationNumber);
    }
}