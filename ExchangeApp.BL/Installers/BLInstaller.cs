using AutoMapper.Internal;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeApp.BL.Installers;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(classes => classes.AssignableTo(typeof(IFacade<,,>)))
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
        
        //services.AddAutoMapper((serviceProvider, cfg) =>
        //{
        //    cfg.Internal().MethodMappingEnabled = false;
        //}, typeof(BusinessLogic).Assembly);

        return services;
    }
}