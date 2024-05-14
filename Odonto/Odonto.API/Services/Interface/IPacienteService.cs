using Odonto.API.Models;

namespace Odonto.API.Services.Interface
{
    public interface IPacienteService
    {
        IEnumerable<Paciente> BuscarTodosPacientes();
        Paciente BuscarPacientePorId(int id);
        Paciente CadastrarPaciente(Paciente paciente);
        Paciente AtualizarPaciente(Paciente paciente);
        Paciente ExcluirPaciente(int id);

    }
}
