using ExchangeApp.DAL.Entities;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ITotalBalanceRepository : IRepository<TotalBalanceEntity, Guid>
{
    new Task<int> InsertAsync(TotalBalanceEntity entity);
    Task<DateTime> GetLastTotalBalanceDate(TotalBalanceType type);
    Task<IEnumerable<TotalBalanceEntity>> GetFilteredAsync(TotalBalanceFilterOption option, DateTime? dateFrom, DateTime? dateUntil);
    Task<bool> ExistsOperationAfterLastDailyTotalBalance();
}