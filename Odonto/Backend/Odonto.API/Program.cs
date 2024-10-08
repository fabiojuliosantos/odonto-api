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

#endregion Variaveis

#region Servicos


#region Configuracao Roles

builder.Services.ResolveDependecies(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

#endregion Configuracao Roles

#region Injecao de Dependencia

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

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();