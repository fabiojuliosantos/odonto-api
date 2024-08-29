﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Odonto.Application.Interfaces;
using Odonto.Application.Services;
using Odonto.Infra.Context;
using Odonto.Infra.Identity;
using Odonto.Infra.Interfaces;
using Odonto.Infra.Repositories;
using System.Text;

namespace Odonto.IoC;

public static class DependecyInjectionConfig
{
    public static IServiceCollection ResolveDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        string conectionString = configuration.GetConnectionString("DefaultConnection");

        #region Configuracao Identity

        services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();

        #endregion Configuracao Identity

        #region Configuracao JWT

        var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentException("Chave secreta não foi encontrada!");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey))
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Você não está autenticado" });
                    return context.Response.WriteAsync(result);
                }
            };
        });

        #endregion Configuracao JWT

        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(conectionString); });

        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IDentistaRepository, DentistaRepository>();
        services.AddScoped<IConsultaRepository, ConsultaRepository>();

        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IDentistaService, DentistaService>();
        services.AddScoped<IConsultaService, ConsultaService>();

        services.AddScoped<ITokenService, TokenService>();

        var handlers = AppDomain.CurrentDomain.Load("Odonto.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(handlers));

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("direcao", "secretaria"));
            options.AddPolicy("DentistasEnfermeiros", policy => policy.RequireRole("dentista", "enfermagem"));
            options.AddPolicy("GerenciaTI", policy => policy.RequireRole("gerenciaTI"));
            options.AddPolicy("Gestao", policy => policy.RequireRole("gerenciaTI", "direcao"));
            options.AddPolicy("DentistasDirecao", policy => policy.RequireRole("dentista", "direcao"));
        });
        return services;
    }
}