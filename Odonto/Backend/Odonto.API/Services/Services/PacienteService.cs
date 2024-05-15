using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Services.Interface;

namespace Odonto.API.Services.Services;

public class PacienteService : IPacienteService
{
    IPacienteRepository _repository;

    public PacienteService(IPacienteRepository repository)
    {
        _repository = repository;
    }

    #region Buscar
    public IEnumerable<Paciente> BuscarTodosPacientes()
    {
        var pacientes = _repository.BuscarTodos();
        return pacientes;
    }
    public Paciente BuscarPacientePorId(int id)
    {
        var paciente = _repository.BuscarPacientePeloIdConsulta(id);
        if (paciente is null) throw new Exception($"Paciente de id: {id} não foi encontrado!");
        return paciente;
    }

    #endregion Buscar

    #region Cadastrar
    public Paciente CadastrarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram informados dados para o paciente!");
        _repository.Cadastrar(paciente);
        return paciente;
    }

    #endregion Cadastrar

    #region Atualizar

    public Paciente AtualizarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram informados dados para o paciente!");
        _repository.Atualizar(paciente);
        return paciente;
    }

    #endregion Atualizar

    #region Excluir

    public Paciente ExcluirPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram informados dados para o paciente!");
        _repository.Deletar(paciente);
        return paciente;
    }

    #endregion Excluir
}
