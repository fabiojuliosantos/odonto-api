using MediatR;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Consultas.Queries;

public class BuscarTodasConsultasQuery : IRequestHandler<BuscarTodasConsultasCommand, IEnumerable<Consulta>>
{
    private readonly IConsultaRepository _repository;

    public BuscarTodasConsultasQuery(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Consulta>> Handle(BuscarTodasConsultasCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Consulta> consultas = await _repository.BuscarTodasConsultas();
        return consultas;
    }
}
