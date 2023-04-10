using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExchangeApp.App.Installers;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseName = configuration["ExchangeApp:DAL:SqLite:DatabaseName"];

        if (databaseName is null)
        {
            throw new InvalidOperationException("Database name is not set");
        }
        
        services.AddSingleton<IDbContextFactory<ExchangeAppDbContext>>(provider =>
            new DbContextSqLiteFactory(databaseName));
        services.AddSingleton<IDbMigrator, SqLiteDbMigrator>();

        return services;
    }
}
