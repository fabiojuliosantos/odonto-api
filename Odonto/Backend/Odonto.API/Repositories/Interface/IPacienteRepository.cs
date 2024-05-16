using Odonto.API.Models;
using Odonto.API.Pagination;
using X.PagedList;

namespace Odonto.API.Repositories.Interface;

public interface IPacienteRepository : IRepository<Paciente>
{
    Task<Paciente> BuscarPacientePeloIdConsultaAsync(int id);
    Task<IPagedList<Paciente>> PacientesPaginados(PacientesParameters param);
}