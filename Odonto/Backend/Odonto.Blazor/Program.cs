using Blazored.LocalStorage;
using Odonto.Blazor.Components;
using Odonto.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient(); // Adiciona o HttpClient para requisições HTTP
builder.Services.AddBlazoredLocalStorage(); // Adiciona o Blazored.LocalStorage
builder.Services.AddScoped<LoginService>(); // Registra o LoginService
builder.Services.AddScoped<PacientesService>();//Registra o PacientesService


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7061") });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
