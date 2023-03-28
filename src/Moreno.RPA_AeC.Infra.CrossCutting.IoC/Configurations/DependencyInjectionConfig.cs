﻿using Microsoft.Extensions.DependencyInjection;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Domain.Services;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;
using Moreno.RPA_AeC.Infra.Data.Repository;
using Moreno.RPA_AeC.Infra.Data.UoW;

namespace Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        return services
                .AddApplicationBase()
                .AddAutoMapper()
                .AddRepositories()
                .AddServices();
    }

    private static IServiceCollection AddApplicationBase(this IServiceCollection services)
    {
        return services
            .AddScoped<RPA_AeCDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddTransient<IPesquisaRepository, PesquisaRepository>();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<IPesquisaService, PesquisaService>();
}
