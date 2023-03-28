using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class AutoMapperConfig
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc => { });
        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

}
