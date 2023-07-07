using AutoMapper;
using System.Reflection;
using CommunityToolkit.Maui;
using ExchangeApp.App.Installers;
using ExchangeApp.BL.Installers;
using ExchangeApp.BL.MapperProfiles;
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
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Sono-Regular.ttf", "SonoRegular");
                fonts.AddFont("Sono-SemiBold.ttf", "SonoSemibold");
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

#if DEBUG
        builder.Logging.AddDebug();
#endif

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
