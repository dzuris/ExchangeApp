using System.Reflection;
using ExchangeApp.App.Installers;
using ExchangeApp.BL.Installers;
using Microsoft.Extensions.Configuration;

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

        builder.Services
            .AddDALServices(builder.Configuration)
            .AddAppServices()
            .AddBLServices();

        var app = builder.Build();

        app.Services.GetRequiredService<IDbMigrator>().Migrate();

        return app;
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
}
