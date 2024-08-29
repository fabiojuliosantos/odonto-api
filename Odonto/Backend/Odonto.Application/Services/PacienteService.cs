using Odonto.Application.Interfaces;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using Odonto.Infra.Interfaces;
using X.PagedList;

namespace Odonto.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _repository;

    public PacienteService(IPacienteRepository repository)
    {
        _repository = repository;
    }
    #region Cadastrar

    public async Task<Paciente> CadastrarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram informados dados para o paciente!");
        await _repository.CadastrarNovo(paciente);
        return paciente;
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Paciente> AtualizarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram informados dados para o paciente!");
        await _repository.AtualizarPaciente(paciente);
        return paciente;
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Paciente> ExcluirPaciente(int id)
    {
        Paciente paciente = await _repository.BuscarPorId(id);
        if (id < 1) throw new Exception("Não foram informados dados para o paciente!");
        await _repository.ExcluirPaciente(id);
        return paciente;
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync()
    {
        var pacientes = await _repository.BuscarTodos();
        return pacientes;
    }

    public async Task<Paciente> BuscarPacientePorIdAsync(int id)
    {
        var paciente = await _repository.BuscarPorId(id);
        if (paciente is null) throw new Exception($"Paciente de id: {id} não foi encontrado!");
        return paciente;
    }

    #endregion Buscar
}
