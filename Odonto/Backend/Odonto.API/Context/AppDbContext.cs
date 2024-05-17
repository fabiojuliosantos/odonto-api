using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Odonto.API.Models;

namespace Odonto.API.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Consulta>? Consultas { get; set; }
    public DbSet<Dentista>? Dentistas { get; set; }
    public DbSet<Paciente>? Pacientes { get; set; }
}