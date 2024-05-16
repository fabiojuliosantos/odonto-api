using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Odonto.API.Context;
using Odonto.API.DTOs.Mappings;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;
using Odonto.API.Services.Interface;
using Odonto.API.Services.Services;

#region Variaveis

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#endregion Variaveis

#region Servicos

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