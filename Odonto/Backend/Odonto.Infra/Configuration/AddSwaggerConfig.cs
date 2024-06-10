using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Odonto.Infra.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API do Centro de Odontologia Especializada",
                Description = "API de controle de pacientes, dentistas e consultas",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Fabio Julio",
                    Email = "fabiojulio.santos@pm.me"
                }
            });
        });
        return services;
    }
}