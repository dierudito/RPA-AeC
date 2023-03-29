using Microsoft.Extensions.DependencyInjection;
using Moreno.RPA_AeC.Application.AppService;
using Moreno.RPA_AeC.Application.Interfaces;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Domain.Services;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;
using Moreno.RPA_AeC.Infra.Data.Repository;
using Moreno.RPA_AeC.Infra.Data.UoW;
using Moreno.RPA_AeC.Infra.Rpa.Interfaces;
using Moreno.RPA_AeC.Infra.Rpa.WebControls;

namespace Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        return services
                .AddApplicationService()
                .AddApplicationBase()
                .AddRepositories()
                .AddServices()
                .AddRpa();
    }

    private static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        return services.AddTransient<IPesquisaAppService, PesquisaAppService>();
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

    private static IServiceCollection AddRpa(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        return services
            .AddTransient<ISeleniumRpa, SeleniumRpa>();
    }
}
