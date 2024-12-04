using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;

namespace Odonto.Application.Interfaces;

public interface IPacienteService
{
    Task<Paciente> BuscarPacientePorIdAsync(BuscarPacientePorIdCommand command);
    Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync();
    Task<Paciente> CadastrarPaciente(CadastrarPacienteCommand command);
    Task<Paciente> AtualizarPaciente(AtualizarPacienteCommand command);
    Task<Paciente> ExcluirPaciente(ExcluirPacienteCommand command);
}
