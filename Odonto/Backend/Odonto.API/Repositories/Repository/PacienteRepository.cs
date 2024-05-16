using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Pagination;
using Odonto.API.Repositories.Interface;
using X.PagedList;

namespace Odonto.API.Repositories.Repository;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Paciente> BuscarPacientePeloIdConsultaAsync(int id)
    {
        var paciente = await _context.Pacientes.Include(p => p.Consultas)
            .FirstOrDefaultAsync(p => p.PacienteId == id);
        return paciente;
    }

  
    public async Task<IPagedList<Paciente>> PacientesPaginados(PacientesParameters param)
    {
        var pacientes = await BuscarTodosAsync();
        var pacientesOrdenados = pacientes.OrderBy(p => p.PacienteId).AsQueryable();
        var pacientesRetorno = await pacientesOrdenados.ToPagedListAsync(param.PageNumber, param.PageSize);

        return pacientesRetorno;
    }
}