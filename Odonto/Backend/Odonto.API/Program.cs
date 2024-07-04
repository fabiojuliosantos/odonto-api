using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Odonto.API.DTOs.Mappings;
using Odonto.Infra.Context;
using Odonto.Infra.Identity;
using Odonto.IoC;
using Odonto.IoC.Configuration;
using System.Text;
using System.Text.Json.Serialization;

#region Variaveis

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var secretKey = builder.Configuration["Jwt:SecretKey"] ??
                throw new ArgumentException("Chave secreta inválida!");

#endregion Variaveis

#region Servicos

#region Configuracao Identity

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

#endregion Configuracao Identity

#region Configuracao JWT

builder.Services.AddAuthentication(options =>
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
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
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

#region Configuracao Roles

builder.Services.ResolveDependecies();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Dentistas", policy => policy.RequireRole("Dentista"));
    options.AddPolicy("Recepcao", policy => policy.RequireRole("Recepcao"));
});

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(conectionString); });

#endregion Configuracao Roles

#region Injecao de Dependencia

builder.Services.ResolveDependecies();

#endregion Injecao de Dependencia

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(OdontoDTOMappingProfile));

#endregion Servicos

#region Configuracao do Swagger

builder.Services.AddSwaggerConfig();

#endregion Configuracao do Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();