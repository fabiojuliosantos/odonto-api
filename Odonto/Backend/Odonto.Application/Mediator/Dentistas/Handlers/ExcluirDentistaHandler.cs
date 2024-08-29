using MediatR;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Handlers;

public class ExcluirDentistaHandler : IRequestHandler<ExcluirDentistaCommand, Dentista>
{
    private readonly IDentistaRepository _repository;

    public ExcluirDentistaHandler(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dentista> Handle(ExcluirDentistaCommand request, CancellationToken cancellationToken)
    {
        Dentista dentista = new Dentista() { DentistaId = request.DentistaId};

        return await _repository.ExcluirDentista(dentista.DentistaId);
    }
}
