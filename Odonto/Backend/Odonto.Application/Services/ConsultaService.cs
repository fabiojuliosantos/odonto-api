﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Odonto.Application.DTO;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Application.TratarErros;
using Odonto.Domain.Entities;

namespace Odonto.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IMediator _mediator;

    public ConsultaService(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Cadastrar

    public async Task<Consulta> CadastrarConsulta(CadastrarConsultaDTO dto)
    {
        try
        {
            if (dto is null) throw new CustomException("Dados para consulta não foram informados!", StatusCodes.Status400BadRequest);

            CadastrarConsultaCommand consultaObjeto = new CadastrarConsultaCommand 
            {
                DataConsulta = dto.DataConsulta,
                DentistaId = dto.DentistaId,
                PacienteId = dto.PacienteId,
                Descricao = dto.Descricao
            };

            Consulta consulta = await _mediator.Send(consultaObjeto);

            return consulta;
        }
        catch (CustomException)
        {
            throw;
        }
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Consulta> AtualizarConsulta(AtualizarConsultaCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Dados para consulta não foram informados!", StatusCodes.Status400BadRequest);

            Consulta consulta = await _mediator.Send(command);

            return consulta;
        }
        catch (CustomException)
        {
            throw;
        }
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Consulta> ExcluirConsulta(ExcluirConsultaCommand command)
    {
        try
        {
            if (command.ConsultaId < 1) throw new CustomException("Dados para consulta não foram informados!", StatusCodes.Status400BadRequest);

            Consulta consulta = await _mediator.Send(command);

            return consulta;
        }
        catch (CustomException) 
        { 
            throw; 
        }
        
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync()
    {
        try
        {
            BuscarTodasConsultasCommand command = new BuscarTodasConsultasCommand();
            
            IEnumerable<Consulta> consultas = await _mediator.Send(command);
            
            return consultas;
        }
        catch(CustomException ex)
        {
            throw;
        }
    }

    public async Task<Consulta> BuscarConsultaPorIdAsync(BuscarConsultaPorIdCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Dados para consulta não foram informados!", StatusCodes.Status400BadRequest);
            
            Consulta consulta = await _mediator.Send(command);
    
            return consulta;
        }

        catch(CustomException ex)
        {
            throw;
        }
    }

    #endregion Buscar
}
