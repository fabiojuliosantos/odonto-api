using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Infra.Interfaces;

public interface IPacienteRepository : IRepository<Paciente>
{
    Task<Paciente> BuscarPacientePeloIdConsultaAsync(int id);
    Task<IPagedList<Paciente>> PacientesPaginados(PacientesParameters param);
}