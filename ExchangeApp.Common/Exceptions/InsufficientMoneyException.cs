namespace ExchangeApp.Common.Exceptions;

public class InsufficientMoneyException : Exception
{
    public InsufficientMoneyException(string? message = null) : base(message)
    {
    }
}