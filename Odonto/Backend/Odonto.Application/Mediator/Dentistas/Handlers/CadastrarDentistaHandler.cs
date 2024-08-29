using MediatR;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Handlers;
public class CadastrarDentistaHandler : IRequestHandler<CadastrarDentistaCommand, Dentista>
{
    private readonly IDentistaRepository _repository;

    public CadastrarDentistaHandler(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dentista> Handle(CadastrarDentistaCommand request, CancellationToken cancellationToken)
    {
        Dentista dentista = new Dentista() 
        {
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone,
            Cro = request.Cro
        };
        return await _repository.CadastrarNovo(dentista);
    }
}
