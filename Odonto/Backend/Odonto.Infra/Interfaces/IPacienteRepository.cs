using Odonto.Domain.Entities;

namespace Odonto.Infra.Interfaces;

public interface IPacienteRepository
{
    Task<IEnumerable<Paciente>> BuscarTodos();
    Task<Paciente> BuscarPorId(int id);
    Task<Paciente> CadastrarNovo(Paciente paciente);
    Task<Paciente> AtualizarPaciente(Paciente paciente);
    Task<Paciente> ExcluirPaciente(int id);
}