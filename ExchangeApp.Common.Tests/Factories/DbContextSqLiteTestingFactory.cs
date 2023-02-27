using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory : IDbContextFactory<ExchangeAppDbContext>
{
    private readonly string _databaseNameWithPath;
    private readonly bool _seedTestingData;

    public DbContextSqLiteTestingFactory(string databaseNameWithPath, bool seedTestingData)
    {
        _databaseNameWithPath = databaseNameWithPath;
        _seedTestingData = seedTestingData;
    }

    public ExchangeAppDbContext CreateDbContext()
    {
        if (!File.Exists(_databaseNameWithPath))
        {
            throw new FileNotFoundException("Database file not found.", _databaseNameWithPath);
        }

        var builder = new DbContextOptionsBuilder<ExchangeAppDbContext>();
        builder.UseSqlite($"Data Source={_databaseNameWithPath};Cache=Shared");

        return new ExchangeAppTestingDbContext(builder.Options, _seedTestingData);
    }
}