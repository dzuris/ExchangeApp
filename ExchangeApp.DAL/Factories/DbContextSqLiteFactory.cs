using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ExchangeAppDbContext>
{
    private readonly string _databaseName;

    public DbContextSqLiteFactory(string databaseName)
    {
        _databaseName = databaseName;
    }

    public ExchangeAppDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ExchangeAppDbContext> builder = new();
        builder.UseSqlite($"Data source={_databaseName};Cache=Shared");

        return new ExchangeAppDbContext(builder.Options);
    }
}