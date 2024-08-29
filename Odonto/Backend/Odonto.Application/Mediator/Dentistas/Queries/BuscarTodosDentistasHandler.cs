using MediatR;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Queries;

public class BuscarTodosDentistasHandler : IRequestHandler<BuscarTodosDentistasCommand, IEnumerable<Dentista>>
{
    private readonly IDentistaRepository _repository;

    public BuscarTodosDentistasHandler(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Dentista>> Handle(BuscarTodosDentistasCommand request, CancellationToken cancellationToken)
    {
        return await _repository.BuscarTodos();
    }
}
