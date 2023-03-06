namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface IOperationRepository
{
    Task<IEnumerable<object>> GetOperationsAsync(int pageSize, int pageNumber);
}