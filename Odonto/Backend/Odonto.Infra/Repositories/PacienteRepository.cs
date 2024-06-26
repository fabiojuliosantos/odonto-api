using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using Odonto.Infra.Context;
using Odonto.Infra.Interfaces;
using X.PagedList;

namespace Odonto.Infra.Repositories;

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