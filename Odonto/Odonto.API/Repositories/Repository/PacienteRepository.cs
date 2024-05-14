using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class PacienteRepository : IPacienteRepository
{
    AppDbContext _context;
    public PacienteRepository(AppDbContext context)
    {
        _context = context;
    }
    #region Buscar
    public Paciente BuscarPacientePorId(int id)
    {
        var paciente = _context.Pacientes.FirstOrDefault(p => p.PacienteId == id);
        
        return paciente;
    }

    public IEnumerable<Paciente> BuscarTodosPacientes()
    {
        var pacientes = _context.Pacientes.ToList();

        return pacientes;
    }
    #endregion Buscar

    #region Cadastrar
    
    public Paciente CadastrarPaciente(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        
        _context.SaveChanges();
        
        return paciente;
    }
    
    #endregion Cadastrar

    #region Atualização
    public Paciente AtualizarPaciente(Paciente paciente)
    {
        _context.Entry(paciente).State = EntityState.Modified;
        
        _context.SaveChanges();
        
        return paciente;
    }
    #endregion Atualização

    #region Excluir
    public Paciente ExcluirPaciente(int id)
    {
        var paciente = _context.Pacientes.FirstOrDefault(p => p.PacienteId == id);

        _context.Pacientes.Remove(paciente);
        _context.SaveChanges();
        return paciente;
    }
    #endregion Excluir
}
