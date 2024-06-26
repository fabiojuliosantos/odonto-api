using Microsoft.Extensions.DependencyInjection;
using Odonto.Application.Interfaces;
using Odonto.Application.Services;
using Odonto.Infra.Interfaces;
using Odonto.Infra.Repositories;

namespace Odonto.IoC;

public static class DependecyInjectionConfig
{
    public static IServiceCollection ResolveDependecies(this IServiceCollection services)
    {
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IDentistaRepository, DentistaRepository>();
        services.AddScoped<IConsultaRepository, ConsultaRepository>();

        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IDentistaService, DentistaService>();
        services.AddScoped<IConsultaService, ConsultaService>();
        
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}