using MediatR;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Pacientes.Queries;
public class BuscarPacientePorId : IRequestHandler<BuscarPacientePorIdCommand, Paciente>
{
    private readonly IPacienteRepository _repository;

    public BuscarPacientePorId(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(BuscarPacientePorIdCommand request, CancellationToken cancellationToken)
    {
        Paciente pacienteBusca = new Paciente()
        {
            PacienteId = request.PacienteId,
        };
        return await _repository.BuscarPorId(pacienteBusca.PacienteId);
    }
}
