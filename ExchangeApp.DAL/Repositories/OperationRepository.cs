using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly DbContext _dbContext;

    public OperationRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IEnumerable<object>> GetOperationsAsync(int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }
}