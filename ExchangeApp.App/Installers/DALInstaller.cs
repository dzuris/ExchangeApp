using System.Threading.Tasks.Dataflow;
using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Factories;
using ExchangeApp.DAL.Repositories;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scrutor;

namespace ExchangeApp.App.Installers;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseName = configuration["ExchangeApp:DAL:Sqlite:DatabaseName"];

        if (databaseName == null)
        {
            throw new InvalidOperationException("Database name is not set");
        }

        services.AddSingleton<IDbContextFactory<ExchangeAppDbContext>>(provider =>
            new DbContextSqLiteFactory(databaseName));
        services.AddSingleton<IDbMigrator, SqliteDbMigrator>();

        //services.Scan(selector =>
        //    selector.FromAssemblyOf<CurrencyRepository>()
        //        .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
        //        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        //        .AsMatchingInterface()
        //        .WithScopedLifetime()
        //);

        return services;
    }
}
