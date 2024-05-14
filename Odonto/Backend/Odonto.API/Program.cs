using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Odonto.API.Context;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;
using Odonto.API.Services.Interface;
using Odonto.API.Services.Services;

#region Vari�veis
var builder = WebApplication.CreateBuilder(args);
string conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endregion Vari�veis

#region Servi�os

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conectionString);
});

#region Repositories
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped(typeof(IDentistaRepository), typeof(DentistaRepository));
builder.Services.AddScoped(typeof(IConsultaRepository), typeof(ConsultaRepository));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
#endregion Repositories

#region Services
builder.Services.AddScoped<IPacienteService, PacienteService>();

#endregion Services

builder.Services.AddEndpointsApiExplorer();

#endregion Servi�os

#region Configura��o do Swagger
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
                Name = "F�bio J�lio",
                Email = "fabiojulio.santos@pm.me"
            }
        });
    });
#endregion Configura��o do Swagger

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
