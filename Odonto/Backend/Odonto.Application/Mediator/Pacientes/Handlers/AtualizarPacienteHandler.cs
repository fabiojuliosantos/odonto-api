using MediatR;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Pacientes.Handlers;

public class AtualizarPacienteHandler : IRequestHandler<AtualizarPacienteCommand, Paciente>
{
    private readonly IPacienteRepository _repository;

    public AtualizarPacienteHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(AtualizarPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente pacienteAtualizado = new Paciente()
        {
            PacienteId = request.PacienteId,
            Nome = request.Nome,
            Cpf = request.Cpf,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            NumeroCasa = request.NumeroCasa,
            Telefone = request.Telefone,
            Email = request.Email
        };
        await _repository.AtualizarPaciente(pacienteAtualizado);
        return pacienteAtualizado;
    }
}
