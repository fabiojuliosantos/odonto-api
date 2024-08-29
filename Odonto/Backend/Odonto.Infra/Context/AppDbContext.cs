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
    public DbSet<Documento>? Documentos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);


        #region Pacientes
        mb.Entity<Paciente>().HasKey(p => p.PacienteId);
        mb.Entity<Paciente>().Property(p => p.Nome).HasMaxLength(80).IsRequired();
        mb.Entity<Paciente>().Property(p => p.Cpf).HasMaxLength(11).IsRequired();
        mb.Entity<Paciente>().Property(p => p.Telefone).HasMaxLength(11).IsRequired();
        mb.Entity<Paciente>().Property(p => p.Email).HasMaxLength(70);
        mb.Entity<Paciente>().Property(p => p.Cep).HasMaxLength(8);
        mb.Entity<Paciente>().Property(p => p.Logradouro).HasMaxLength(70);
        mb.Entity<Paciente>().Property(p => p.NumeroCasa).HasColumnType("Integer");
        #endregion Pacientes

        #region Dentistas
        mb.Entity<Dentista>().HasKey(d => d.DentistaId);
        mb.Entity<Dentista>().Property(d => d.Nome).HasMaxLength(80).IsRequired();
        mb.Entity<Dentista>().Property(d => d.Telefone).HasMaxLength(11).IsRequired();
        mb.Entity<Dentista>().Property(d => d.Email).HasMaxLength(70);
        mb.Entity<Dentista>().Property(d => d.Cro).HasMaxLength(9);
        #endregion Dentistas

        #region Consultas
        mb.Entity<Consulta>().HasKey(c => c.ConsultaId);
        mb.Entity<Consulta>().Property(c => c.Descricao).HasMaxLength(200).IsRequired();
        mb.Entity<Consulta>().Property(c => c.DataConsulta).HasColumnType("Date");
        mb.Entity<Consulta>().HasOne(c => c.Paciente).WithMany(c => c.Consultas).HasForeignKey(c => c.PacienteId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Consulta>().HasOne(c => c.Dentista).WithMany(c => c.Consultas).HasForeignKey(c => c.DentistaId).OnDelete(DeleteBehavior.Cascade);
        #endregion Consultas

        #region Documentos
        mb.Entity<Documento>().HasKey(doc => doc.DocumentoId);
        mb.Entity<Documento>().Property(doc => doc.DataEmissao).HasColumnType("Date");
        mb.Entity<Documento>().HasOne(doc => doc.Paciente).WithMany(doc => doc.Documentos).HasForeignKey(doc => doc.PacienteId).OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Documento>().HasOne(doc => doc.Dentista).WithMany(doc => doc.Documentos).HasForeignKey(doc => doc.DentistaId).OnDelete(DeleteBehavior.Cascade);
        #endregion Documentos
    }
}