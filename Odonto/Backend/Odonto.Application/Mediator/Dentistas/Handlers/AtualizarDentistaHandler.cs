using MediatR;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Handlers;

public class AtualizarDentistaHandler : IRequestHandler<AtualizarDentistaCommand, Dentista>
{
    private readonly IDentistaRepository _repository;

    public AtualizarDentistaHandler(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dentista> Handle(AtualizarDentistaCommand request, CancellationToken cancellationToken)
    {
        Dentista dentista = new Dentista()
        {
            DentistaId = request.DentistaId,
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone,
            Cro = request.Cro
        };

        return await _repository.AtualizarDentista(dentista);
    }
}
