using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Odonto.API.Context;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;

#region Variáveis
var builder = WebApplication.CreateBuilder(args);
string conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endregion Variáveis

#region Serviços

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conectionString);
});

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

builder.Services.AddEndpointsApiExplorer();

#endregion Serviços

#region Configuração do Swagger
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API do Centro de Odontologia Especializada",
            Description = "API de controle de pacientes e consultas",
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
