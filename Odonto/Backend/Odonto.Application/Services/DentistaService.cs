using MediatR;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Dentistas.Commands;
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
            if (command is null) throw new Exception("Não foram informados dados para o(a) Dentista!");
            return await _mediator.Send(command);
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
            if (command.DentistaId < 0) throw new Exception("Valor de id informado é inválido!");
            return await _mediator.Send(command);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync()
    {
        BuscarTodosDentistasCommand command = new BuscarTodosDentistasCommand();
        return await _mediator.Send(command);
    }

    public async Task<Dentista> CadastrarDentista(CadastrarDentistaCommand command)
    {
        if(command is null) throw new Exception("Não foram informados dados para o(a) dentista!");        
        return await _mediator.Send(command);
    }

    public async Task<Dentista> ExcluirDentista(ExcluirDentistaCommand command)
    {
        if (command.DentistaId < 1) throw new Exception("Valor informado para o id é inválido!");
        return await _mediator.Send(command);
    }
}
