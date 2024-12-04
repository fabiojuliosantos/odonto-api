using MediatR;
using Microsoft.AspNetCore.Http;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Application.TratarErros;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IMediator _mediator;
    public PacienteService(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Cadastrar

    public async Task<Paciente> CadastrarPaciente(CadastrarPacienteCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Não foram informados dados para o paciente!", StatusCodes.Status400BadRequest);
            
            Paciente paciente = await _mediator.Send(command);
            
            return paciente;
        }
        catch (CustomException ex)
        {
            throw;
        }
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Paciente> AtualizarPaciente(AtualizarPacienteCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Não foram informados dados para o paciente!", StatusCodes.Status400BadRequest); ;

            Paciente paciente = await _mediator.Send(command);

            return paciente;
        }
        catch (CustomException ex) 
        {
            throw; 
        }
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Paciente> ExcluirPaciente(ExcluirPacienteCommand command)
    {
        try
        {
            if (command.PacienteId< 1) throw new CustomException("Não foram informados dados para o paciente!", StatusCodes.Status400BadRequest);
            
            Paciente paciente = await _mediator.Send(command);
            
            return paciente;
        }
        catch (CustomException ex) 
        {
            throw; 
        }
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Paciente>> BuscarTodosPacientesAsync()
    {
        try
        {
            BuscarTodosPacientesCommand command = new BuscarTodosPacientesCommand();
            
            var pacientes = await _mediator.Send(command);
            
            return pacientes;
        }
        catch (CustomException ex) 
        {
            throw;
        }
    }

    public async Task<Paciente> BuscarPacientePorIdAsync(BuscarPacientePorIdCommand command)
    {
        try
        {
            Paciente paciente = await _mediator.Send(command);

            if (paciente is null) throw new CustomException($"Paciente de id: {command.PacienteId} não foi encontrado!", StatusCodes.Status400BadRequest);
            
            return paciente;
        }

        catch (CustomException ex)
        {
            throw;
        }
    }

    #endregion Buscar
}
