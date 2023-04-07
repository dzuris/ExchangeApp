using AutoMapper;
using ExchangeApp.BL.MapperProfiles;
using ExchangeApp.Common.Tests.Factories;
using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.BL.Tests.FacadeTests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected IDbContextFactory<ExchangeAppDbContext> DbContextFactory { get; }
    protected IMapper Mapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    protected FacadeTestsBase()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            var profiles = typeof(CurrencyMapperProfile).Assembly
                .GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x))
                .ToList();

            profiles.ForEach(profile =>
            {
                if (Activator.CreateInstance(profile) is Profile instance)
                {
                    cfg.AddProfile(instance);
                }
            });
        });
        Mapper = mapperConfig.CreateMapper();

        DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory, Mapper);
    }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}