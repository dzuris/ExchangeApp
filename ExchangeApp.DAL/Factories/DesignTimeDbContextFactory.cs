using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExchangeApp.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExchangeAppDbContext>
{
    public ExchangeAppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ExchangeAppDbContext> builder = new();
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        path = Path.Combine(path, "ExchangeApp");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var dbPath = Path.Combine(path, "exchangeApp.db");
        builder.UseSqlite($"Data Source={dbPath}");

        return new ExchangeAppDbContext(builder.Options);
    }
}