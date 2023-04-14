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
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Combine(path, "ExchangeApp", _databaseName);

        if (!File.Exists(dbPath))
        {
            throw new FileNotFoundException("Database file not found.", dbPath);
        }

        var optionsBuilder = new DbContextOptionsBuilder<ExchangeAppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.EnableSensitiveDataLogging();
         
        return new ExchangeAppDbContext(optionsBuilder.Options);
    }
}