using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Odonto.IoC.Configuration;

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

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            var xmlFileName = $"Odonto.API.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
        return services;
    }
}