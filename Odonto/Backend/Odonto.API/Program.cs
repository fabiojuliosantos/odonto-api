using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Odonto.API.Context;
using Odonto.API.DTOs.Mappings;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;
using Odonto.API.Services.Interface;
using Odonto.API.Services.Services;

#region Variaveis

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var secretKey = builder.Configuration["JWT:SecretKey"] ?? 
                throw new ArgumentException("Chave secreta inválida!");

#endregion Variaveis

#region Servicos

builder.Services.AddAuthorization();

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
#endregion Configuracao JWT

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(conectionString); });

#region Repositories

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IDentistaRepository, DentistaRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

#endregion Repositories

#region Services

builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IDentistaService, DentistaService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<ITokenService, TokenService>();

#endregion Services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(OdontoDTOMappingProfile));

#endregion Servicos

#region Configuracao do Swagger

builder.Services.AddSwaggerGen(
    c =>
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

#endregion Configuracao do Swagger

#region Configuracao Identity

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#endregion Configuracao Identity

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();