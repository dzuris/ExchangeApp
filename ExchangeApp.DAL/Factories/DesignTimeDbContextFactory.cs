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
        var dbPath = Path.Join(path, "exchangeApp.db");
        builder.UseSqlite($"Data Source={dbPath}");

        return new ExchangeAppDbContext(builder.Options);
    }
}