using MediatR;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Consultas.Handlers;

public class ExcluirConsultaHandler : IRequestHandler<ExcluirConsultaCommand, Consulta>
{
    private readonly IConsultaRepository _repository;

    public ExcluirConsultaHandler(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Consulta> Handle(ExcluirConsultaCommand request, CancellationToken cancellationToken)
    {
        Consulta consulta = new Consulta() { ConsultaId = request.ConsultaId };

        return await _repository.ExcluirConsulta(consulta.ConsultaId);
    }
}
