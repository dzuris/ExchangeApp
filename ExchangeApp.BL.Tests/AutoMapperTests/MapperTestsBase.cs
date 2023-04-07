using AutoMapper;
using ExchangeApp.BL.MapperProfiles;

namespace ExchangeApp.BL.Tests.AutoMapperTests;

public class MapperTestsBase
{
    protected IMapper Mapper { get; }

    public MapperTestsBase()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
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
        Mapper = mapperConfiguration.CreateMapper();
    }
}