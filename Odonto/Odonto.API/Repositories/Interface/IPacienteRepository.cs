using Odonto.API.Models;

namespace Odonto.API.Repositories.Interface;

public interface IPacienteRepository
{
    IEnumerable<Paciente> BuscarTodosPacientes();
    Paciente BuscarPacientePorId(int id);
    Paciente CadastrarPaciente(Paciente paciente);
    Paciente AtualizarPaciente(Paciente paciente);
    Paciente ExcluirPaciente(Paciente paciente);
}
