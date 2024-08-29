using MediatR;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Consultas.Queries;

public class BuscarConsultaPorIdQuery : IRequestHandler<BuscarConsultaPorIdCommand, Consulta>
{
    private readonly IConsultaRepository _repository;

    public async Task<Consulta> Handle(BuscarConsultaPorIdCommand request, CancellationToken cancellationToken)
    {
        Consulta consultaBusca = new Consulta() { ConsultaId = request.ConsultaId };
        return await _repository.BuscarConsultaPorId(consultaBusca.ConsultaId);
    }
}
