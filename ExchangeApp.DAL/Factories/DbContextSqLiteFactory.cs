using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ExchangeAppDbContext>
{
    private readonly DbContextOptionsBuilder<ExchangeAppDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName)
    {
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        //_contextOptionsBuilder.EnableSensitiveDataLogging();
        //_contextOptionsBuilder.LogTo(Console.WriteLine);
    }

    public ExchangeAppDbContext CreateDbContext() => new(_contextOptionsBuilder.Options);
}