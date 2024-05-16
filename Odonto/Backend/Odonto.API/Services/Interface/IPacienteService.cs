using Odonto.API.Models;
using Odonto.API.Pagination;
using X.PagedList;

namespace Odonto.API.Services.Interface;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync();
    Task<IPagedList<Paciente>> PacientesPaginadosAsync(PacientesParameters param);
    Task<Paciente> BuscarPacientePorIdAsync(int id);

    Paciente CadastrarPaciente(Paciente paciente);
    Paciente AtualizarPaciente(Paciente paciente);
    Paciente ExcluirPaciente(Paciente paciente);
}