using MediatR;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Pacientes.Commands;

public class AtualizarPacienteCommandHandler : IRequestHandler<AtualizarPacienteCommand, Paciente>
{
    private readonly IPacienteRepository _repository;
    public AtualizarPacienteCommandHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }
    public async Task<Paciente> Handle(AtualizarPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente pacienteExistente = await _repository.BuscarPorIdAsync( p => p.PacienteId == request.PacienteId);
        
        if (pacienteExistente == null) throw new Exception("Paciente não encontrado!");
        
        Paciente paciente = new Paciente()
        {
            PacienteId = pacienteExistente.PacienteId,
            Nome = request.Nome,
            DataNascimento = request.DataNascimento,
            Email = request.Email,
            Telefone = request.Telefone,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Bairro = request.Bairro,
            NumeroCasa = request.NumeroCasa
        };

        _repository.Atualizar(paciente);
        
        return paciente;
    }
}
