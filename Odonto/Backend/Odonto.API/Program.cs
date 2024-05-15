using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Odonto.API.Context;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;
using Odonto.API.Services.Interface;
using Odonto.API.Services.Services;
using System.Text.Json.Serialization;

#region Variáveis

var builder = WebApplication.CreateBuilder(args);
string conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#endregion Variáveis

#region Serviços

builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions
               .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conectionString);
});

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

#endregion Serviços

#region Configuração do Swagger
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
                Name = "Fábio Júlio",
                Email = "fabiojulio.santos@pm.me"
            }
        });
    });
#endregion Configuração do Swagger

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
