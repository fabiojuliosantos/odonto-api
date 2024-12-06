using MediatR;
using Microsoft.AspNetCore.Http;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Application.Mediator.Dentistas.Queries;
using Odonto.Application.TratarErros;
using Odonto.Domain.Entities;

namespace Odonto.Application.Services;

public class DentistaService : IDentistaService
{
    private readonly IMediator _mediator;

    public DentistaService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Dentista> AtualizarDentista(AtualizarDentistaCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Não foram informados dados para o(a) Dentista!", StatusCodes.Status400BadRequest);
            
            Dentista dentista = await _mediator.Send(command);

            if (dentista is null) throw new CustomException($"Dentista de id: {command.DentistaId} não pôde ser atualizado!", StatusCodes.Status400BadRequest);
            
            return dentista;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Dentista> BuscarPorIdAsync(BuscarDentistaPorIdCommand command)
    {
        try
        {
            if (command.DentistaId < 0) throw new CustomException("Valor de id informado é inválido!", StatusCodes.Status400BadRequest);

            Dentista dentista = await _mediator.Send(command);

            if (dentista is null) throw new CustomException($"Dentista de id: {command.DentistaId} não foi encontrado!", StatusCodes.Status404NotFound);
            
            return dentista;
        }
        catch (CustomException ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync()
    {
        try
        {
            BuscarTodosDentistasCommand command = new BuscarTodosDentistasCommand();
            
            return await _mediator.Send(command);
        }
        catch (CustomException)
        {
            throw;
        }
    }

    public async Task<Dentista> CadastrarDentista(CadastrarDentistaCommand command)
    {
        try
        {
            if (command is null) throw new CustomException("Não foram informados dados para o(a) dentista!", StatusCodes.Status400BadRequest);
            
            Dentista dentista = await _mediator.Send(command);

            if (dentista is null) throw new CustomException("Não foi possível cadastrar um novo dentista!", StatusCodes.Status400BadRequest);
            
            return dentista;
        }
        catch (CustomException)
        {
            throw;
        }
        
    }

    public async Task<Dentista> ExcluirDentista(ExcluirDentistaCommand command)
    {
        try
        {
            if (command.DentistaId < 1) throw new CustomException("Valor informado para o id é inválido!", StatusCodes.Status400BadRequest);
            
            var dentista = await _mediator.Send(command);
            
            return dentista;
        }
        catch(CustomException)
        {
            throw;
        }
    }

    public async Task<Dentista> BuscarDentistaEmail(BuscarDentistasEmailQuery query)
    {
        try
        {
            if (query == null) throw new CustomException("Dentista não informado!", StatusCodes.Status400BadRequest);
        
            Dentista dentista = await _mediator.Send(query);
        
            return dentista;
        }
        catch (CustomException) 
        {
            throw;
        }
    }
}
