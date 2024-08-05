using MediatR;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using Odonto.Infra.Repositories;

namespace Odonto.Application.Pacientes.Commands;

public class ExcluirPacienteCommandHandler : IRequestHandler<ExcluirPacienteCommand, Paciente>
{
    private readonly IPacienteRepository _repository;

    public ExcluirPacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(ExcluirPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente novoPaciente = new Paciente()
        {
            PacienteId = request.PacienteId,
            Nome = request.Nome,
            DataNascimento = request.DataNascimento,
            Email = request.Email,
            Telefone = request.Telefone
        };

        _repository.Deletar(novoPaciente);

        return novoPaciente;
    }
    
}
