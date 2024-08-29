using MediatR;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Consultas.Handlers;

public class AtualizarConsultaHandler : IRequestHandler<AtualizarConsultaCommand, Consulta>
{
    private readonly IConsultaRepository _repository;

    public AtualizarConsultaHandler(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Consulta> Handle(AtualizarConsultaCommand request, CancellationToken cancellationToken)
    {
        Consulta consultaAtualizada = new Consulta()
        {
            ConsultaId = request.ConsultaId,
            Descricao = request.Descricao,
            DataConsulta = request.DataConsulta,
            DentistaId = request.DentistaId,
            PacienteId = request.PacienteId
        };
        await _repository.AtualizarConsulta(consultaAtualizada);
        return consultaAtualizada;
    }
}
