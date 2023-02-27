using AutoMapper;
using System.Reflection;
using AutoMapper.Internal;
using ExchangeApp.App.Installers;
using ExchangeApp.BL.Installers;
using ExchangeApp.BL.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExchangeApp.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        ConfigureAppSettings(builder);

        ConfigureLogging(builder.Services);

        ConfigureAutoMapper(builder.Services);

        builder.Services
            .AddDALServices(builder.Configuration)
            .AddAppServices()
            .AddBLServices();

        var app = builder.Build();

        ValidateAutoMapperConfiguration(app.Services);

        app.Services.GetRequiredService<IDbMigrator>().Migrate();

        return app;
    }

    private static void ConfigureLogging(IServiceCollection builderServices)
    {
        builderServices.AddLogging(builder =>
        {
            builder.AddDebug();
            builder.AddConsole();
        });
    }

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "ExchangeApp.App.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }

    private static void ConfigureAutoMapper(IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(CurrencyMapperProfile).Assembly);
    }

    private static void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
    {
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
