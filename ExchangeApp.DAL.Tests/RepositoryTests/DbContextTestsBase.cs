using AutoMapper;
using ExchangeApp.BL.MapperProfiles;
using ExchangeApp.Common.Tests.Factories;
using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Tests.RepositoryTests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected IDbContextFactory<ExchangeAppDbContext> DbContextFactory { get; }
    protected ExchangeAppDbContext ExchangeAppDbContextSUT { get; }
    protected IMapper Mapper { get; }

    protected DbContextTestsBase()
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

        ExchangeAppDbContextSUT = DbContextFactory.CreateDbContext();
    }

    public async Task InitializeAsync()
    {
        await ExchangeAppDbContextSUT.Database.EnsureDeletedAsync();
        await ExchangeAppDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ExchangeAppDbContextSUT.Database.EnsureDeletedAsync();
        await ExchangeAppDbContextSUT.DisposeAsync();
    }
}