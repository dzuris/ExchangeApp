using System.Text.RegularExpressions;

namespace ExchangeApp.BL.Utilities;

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
                @"^[0-9]{2}((0[1-9])|([1-9][0-2])|([2][1-9])|([3][0-2])|([5][1-9])|([6][0-2])|([7][1-9])|([8][0-2]))(0[1-9]|[1-2][0-9]|3[0-1])(\s*/\s*|\s*)?\d{1,4}$");
        return identificationNumberRegex.IsMatch(identificationNumber);
    }
}