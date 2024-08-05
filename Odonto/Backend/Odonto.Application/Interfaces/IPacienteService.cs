using Odonto.Application.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync();
    Task<IPagedList<Paciente>> PacientesPaginadosAsync(PacientesParameters param);
    Task<Paciente> BuscarPacientePorIdAsync(int id);

    Task<Paciente> CadastrarPaciente(CadastrarPacienteCommand command);
    Task<Paciente> AtualizarPaciente(AtualizarPacienteCommand command);
    Task<Paciente> ExcluirPaciente(ExcluirPacienteCommand command);
}
