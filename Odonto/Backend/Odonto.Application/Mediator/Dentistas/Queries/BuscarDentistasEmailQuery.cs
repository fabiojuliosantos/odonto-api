using MediatR;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Dentistas.Queries;

public class BuscarDentistasEmailQuery : IRequest<Dentista>
{
    public string DentistaEmail { get; set; }
}

public class BuscarDentistasEmailQueryHander : IRequestHandler<BuscarDentistasEmailQuery, Dentista>
{
    private readonly IDentistaRepository _repository;

    public BuscarDentistasEmailQueryHander(IDentistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dentista> Handle(BuscarDentistasEmailQuery request, CancellationToken cancellationToken)
    {
        Dentista dentista = await _repository.BuscarPorEmail(request.DentistaEmail);

        return dentista;
    }
}
