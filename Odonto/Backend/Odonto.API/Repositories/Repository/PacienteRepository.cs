using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext context) : base(context)
    {
    }

    public Paciente BuscarPacientePeloIdConsulta(int id)
    {
        var paciente = _context.Pacientes.Include(p => p.Consultas)
                                         .FirstOrDefault(p => p.PacienteId == id);
        return paciente;
    }
}
