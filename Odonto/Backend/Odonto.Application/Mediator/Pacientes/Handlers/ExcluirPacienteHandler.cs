using MediatR;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Pacientes.Handlers;

public class ExcluirPacienteHandler: IRequestHandler<ExcluirPacienteCommand, Paciente>
{
    private readonly IPacienteRepository _repository;

    public ExcluirPacienteHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(ExcluirPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente paciente = new Paciente()
        {
            PacienteId = request.PacienteId,
        };
        await _repository.ExcluirPaciente(paciente.PacienteId);
        return paciente;
    }
}
