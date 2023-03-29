using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExchangeApp.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExchangeAppDbContext>
{
    public ExchangeAppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ExchangeAppDbContext> builder = new();
        builder.UseSqlite("Data Source=exchangeApp.db");

        return new ExchangeAppDbContext(builder.Options);
    }
}