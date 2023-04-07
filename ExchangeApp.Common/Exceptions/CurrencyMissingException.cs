namespace ExchangeApp.Common.Exceptions;

public class CurrencyMissingException : Exception
{
    public CurrencyMissingException(string? message = null) : base(message)
    {
    }
}