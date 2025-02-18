﻿using ExchangeApp.App.Services;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.ViewModels;
using ExchangeApp.App.Views;

namespace ExchangeApp.App.Installers;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();
        services.AddTransient<IPrinterService, PrinterService>();

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
            .AsSelf()
            .WithTransientLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<IViewModel>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());

        return services;
    }
}