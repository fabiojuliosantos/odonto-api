using MediatR;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Consultas.Handlers;

public class CadastrarConsultaHandler : IRequestHandler<CadastrarConsultaCommand, Consulta>
{
    private readonly IConsultaRepository _repository;

    public CadastrarConsultaHandler(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Consulta> Handle(CadastrarConsultaCommand request, CancellationToken cancellationToken)
    {
        Consulta consulta = new Consulta()
        {
            DataConsulta = request.DataConsulta,
            Descricao = request.Descricao,
            DentistaId = request.DentistaId,
            PacienteId = request.PacienteId,
        };
        Consulta consultaCadastrada = await _repository.CadastrarConsulta(consulta);
        return consultaCadastrada;
    }
}
