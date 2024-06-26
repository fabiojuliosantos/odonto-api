using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync();
    Task<IPagedList<Paciente>> PacientesPaginadosAsync(PacientesParameters param);
    Task<Paciente> BuscarPacientePorIdAsync(int id);

    Paciente CadastrarPaciente(Paciente paciente);
    Paciente AtualizarPaciente(Paciente paciente);
    Paciente ExcluirPaciente(Paciente paciente);
}
