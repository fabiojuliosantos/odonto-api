using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IPacienteService
{
    Task<Paciente> BuscarPacientePorIdAsync(int id);
    Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync();
    Task<Paciente> CadastrarPaciente(Paciente paciente);
    Task<Paciente> AtualizarPaciente(Paciente paciente);
    Task<Paciente> ExcluirPaciente(int id);
}
