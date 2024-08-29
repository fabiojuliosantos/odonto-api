using MediatR;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Queries;

public class BuscarDentistaPorIdHandler : IRequestHandler<BuscarDentistaPorIdCommand, Dentista>
{
    public IDentistaRepository _repository;

    public BuscarDentistaPorIdHandler(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dentista> Handle(BuscarDentistaPorIdCommand request, CancellationToken cancellationToken)
    {
        return await _repository.BuscarPorId(request.DentistaId);
    }
}
