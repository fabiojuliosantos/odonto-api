using MediatR;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _repository;
    private readonly IMediator _mediator;
    public PacienteService(IPacienteRepository repository,
                           IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    #region Cadastrar

    public async Task<Paciente> CadastrarPaciente(CadastrarPacienteCommand command)
    {
        if (command is null) throw new Exception("Não foram informados dados para o paciente!");
        Paciente paciente = await _mediator.Send(command);
        return paciente;
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Paciente> AtualizarPaciente(AtualizarPacienteCommand command)
    {
        if (command is null) throw new Exception("Não foram informados dados para o paciente!");
        Paciente paciente = await _mediator.Send(command);
        return paciente;
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Paciente> ExcluirPaciente(ExcluirPacienteCommand command)
    {
        if (command.PacienteId< 1) throw new Exception("Não foram informados dados para o paciente!");
        Paciente paciente = await _mediator.Send(command);
        return paciente;
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync()
    {
        BuscarTodosPacientesCommand command = new BuscarTodosPacientesCommand();
        var pacientes = await _mediator.Send(command);
        return pacientes;
    }

    public async Task<Paciente> BuscarPacientePorIdAsync(BuscarPacientePorIdCommand command)
    {
        try
        {
            Paciente paciente = await _mediator.Send(command);
            if (paciente is null) throw new Exception($"Paciente de id: {command.PacienteId} não foi encontrado!");
            return paciente;
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion Buscar
}
