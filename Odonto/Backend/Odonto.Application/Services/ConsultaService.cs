using MediatR;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Consultas.Commands;
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

    public async Task<Consulta> CadastrarConsulta(CadastrarConsultaCommand command)
    {
        if (command is null) throw new Exception("Dados para consulta não foram informados!");

        Consulta consulta = await _mediator.Send(command);

        return consulta;
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Consulta> AtualizarConsulta(AtualizarConsultaCommand command)
    {
        try
        {
            if (command is null) throw new Exception("Dados para consulta não foram informados!");

            Consulta consulta = await _mediator.Send(command);

            return consulta;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Consulta> ExcluirConsulta(ExcluirConsultaCommand command)
    {
        if (command.ConsultaId < 1) throw new Exception("Dados para consulta não foram informados!");

        Consulta consulta = await _mediator.Send(command);

        return consulta;
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync()
    {
        BuscarTodasConsultasCommand command = new BuscarTodasConsultasCommand();
        IEnumerable<Consulta> consultas = await _mediator.Send(command);
        return consultas;
    }

    public async Task<Consulta> BuscarConsultaPorIdAsync(BuscarConsultaPorIdCommand command)
    {
        Consulta consulta = await _mediator.Send(command);
        return consulta;
    }

    #endregion Buscar
}
