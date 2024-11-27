using Odonto.API.DTOs.Mappings;
using Odonto.IoC;
using Odonto.IoC.Configuration;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Sinks.LogBee;
using Serilog.Sinks.LogBee.AspNetCore;
using System.Text.Json.Serialization;

#region Variaveis

var builder = WebApplication.CreateBuilder(args);

#endregion Variaveis

#region Servicos


#region Configuracao Roles

builder.Services.ResolveDependecies(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

#endregion Configuracao Roles

#region Configuracao LogBee
builder.Services.AddSerilog((services, lc) => lc
    .WriteTo.LogBee(new LogBeeApiKey(
            builder.Configuration["LogBee.OrganizationId"]!,
            builder.Configuration["LogBee.ApplicationId"]!,
            builder.Configuration["LogBee.ApiUrl"]!
        ),
        services
    ));
#endregion Configuracao LogBee

#region QuestPDF
QuestPDF.Settings.License = LicenseType.Community;
#endregion QuestPDF

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

//app.UseKissLogMiddleware(options => {
//    options.Listeners.Add(new RequestLogsApiListener(new Application(
//        builder.Configuration["LogBee.OrganizationId"],
//        builder.Configuration["LogBee.ApplicationId"])
//    )
//    {
//        ApiUrl = builder.Configuration["LogBee.ApiUrl"]
//    });
//});
app.UseLogBeeMiddleware();

app.Run();