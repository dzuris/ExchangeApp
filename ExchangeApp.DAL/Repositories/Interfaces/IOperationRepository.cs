using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface IOperationRepository : IRepository<OperationEntityBase, int>
{
    Task<IEnumerable<OperationEntityBase>> GetLastOperationsAsync(int pageSize, int pageNumber);
}