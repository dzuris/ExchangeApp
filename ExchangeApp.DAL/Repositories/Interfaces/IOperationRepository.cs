using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface IOperationRepository : IRepository<OperationEntityBase, int>
{
    Task<IEnumerable<OperationEntityBase>> GetLastOperationsAsync(int pageSize, int pageNumber);
    Task<IEnumerable<OperationEntityBase>> GetOperationsASync(DateTime from, DateTime until);
    Task<IEnumerable<OperationEntityBase>> GetFilteredOperationsAsync(int pageSize, int pageNumber, OperationFilterOption option, int? id, DateTime? from, DateTime? until);
    Task<int> GetTodayOperationsCount();
    Task<IEnumerable<OperationEntityBase>> GetOperationsForStornoUpdate(OperationEntityBase entity);
    Task UpdateAsync (OperationEntityBase entity);
    Task<decimal> GetAverageCourseOfOperationBefore(OperationEntityBase entity);
    Task<IEnumerable<OperationEntityBase>> GetOperationsForProfitCalculationAsync(DateTime from, DateTime until);
}