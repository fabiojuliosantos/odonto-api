using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Entities;
using Odonto.Infra.Identity;


namespace Odonto.Infra.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Consulta>? Consultas { get; set; }
    public DbSet<Dentista>? Dentistas { get; set; }
    public DbSet<Paciente>? Pacientes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}