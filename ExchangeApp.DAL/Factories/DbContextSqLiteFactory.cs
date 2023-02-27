using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ExchangeAppDbContext>
{
    private readonly string _databaseNameWithPath;

    public DbContextSqLiteFactory(string databaseNameWithPath)
    {
        _databaseNameWithPath = databaseNameWithPath;
    }

    public ExchangeAppDbContext CreateDbContext()
    {
        if (!File.Exists(_databaseNameWithPath))
        {
            throw new FileNotFoundException("Database file not found.", _databaseNameWithPath);
        }

        var optionsBuilder = new DbContextOptionsBuilder<ExchangeAppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={_databaseNameWithPath};Cache=Shared");

        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.EnableSensitiveDataLogging();
         
        return new ExchangeAppDbContext(optionsBuilder.Options);
    }
}