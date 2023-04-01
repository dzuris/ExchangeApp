using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Factories;

public class DbContextTestingInMemoryFactory : IDbContextFactory<ExchangeAppTestingDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public ExchangeAppTestingDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ExchangeAppTestingDbContext> contextOptionsBuilder = new();
        contextOptionsBuilder.UseInMemoryDatabase(_databaseName);

        return new ExchangeAppTestingDbContext(contextOptionsBuilder.Options, _seedTestingData);
    }
}