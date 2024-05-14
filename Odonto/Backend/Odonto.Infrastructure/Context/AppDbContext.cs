using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Odonto.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }
        public DbSet<Paciente>? Pacientes { get; set; }
        public DbSet<Dentista>? Dentistas { get; set; }
        public DbSet<Consulta>? Consultas { get; set; }

    }
}
