using Microsoft.Extensions.DependencyInjection;

namespace Odonto.IoC;

public static class DependecyInjectionConfig
{
    public static IServiceCollection ResolveDependecies(this IServiceCollection services)
    {
        //services.AddScoped();
        //services.AddScoped<IPacienteRepository, PacienteRepository>();
     return services;
    }
}