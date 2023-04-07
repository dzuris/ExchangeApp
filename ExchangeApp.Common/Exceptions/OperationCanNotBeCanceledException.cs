namespace ExchangeApp.Common.Exceptions;

public class OperationCanNotBeCanceledException : Exception
{
    public OperationCanNotBeCanceledException(string? message = null) : base(message)
    {
    }
}