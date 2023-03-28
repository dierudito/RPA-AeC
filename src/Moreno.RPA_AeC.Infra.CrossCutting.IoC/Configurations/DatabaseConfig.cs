using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;

namespace Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class DatabaseConfig
{

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        return services
            .AddEntityFrameworkConfiguration();
    }

    private static IServiceCollection AddEntityFrameworkConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<RPA_AeCDbContext>(options =>
        {
            options.UseSqlServer(ConnStrConfig.DefaultConnectionString);
#if (DEBUG)
            options.EnableSensitiveDataLogging();
#endif
        });

        return services;
    }
}
