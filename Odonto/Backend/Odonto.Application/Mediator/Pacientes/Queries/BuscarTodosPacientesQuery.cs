using MediatR;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Pacientes.Queries;

public class BuscarTodosPacientesQuery : IRequestHandler<BuscarTodosPacientesCommand, IEnumerable<Paciente>>
{
    private readonly IPacienteRepository _repository;

    public BuscarTodosPacientesQuery(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Paciente>> Handle(BuscarTodosPacientesCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Paciente> pacientes = await _repository.BuscarTodos();
        return pacientes;
    }
}
