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

    #region Buscar

    public async Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync()
    {
        var pacientes = await _repository.BuscarTodosAsync();
        return pacientes;
    }

    public async Task<IPagedList<Paciente>> PacientesPaginadosAsync(PacientesParameters param)
    {
        var pacientesPaginados = await _repository.PacientesPaginados(param);

        return pacientesPaginados;
    }

    public async Task<Paciente> BuscarPacientePorIdAsync(int id)
    {
        var paciente = await _repository.BuscarPacientePeloIdConsultaAsync(id);
        if (paciente is null) throw new Exception($"Paciente de id: {id} não foi encontrado!");
        return paciente;
    }

    #endregion Buscar
}
