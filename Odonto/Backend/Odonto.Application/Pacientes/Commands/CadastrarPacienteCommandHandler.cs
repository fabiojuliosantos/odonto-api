using MediatR;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Pacientes.Commands;

public class CadastrarPacienteCommandHandler : IRequestHandler<CadastrarPacienteCommand, Paciente>
{
    private readonly IRepository<Paciente> _repository;
    public CadastrarPacienteCommandHandler(IRepository<Paciente> repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(CadastrarPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente novoPaciente = new Paciente()
        {
            Nome = request.Nome,
            DataNascimento = request.DataNascimento,
            Email = request.Email,
            Telefone = request.Telefone,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Bairro = request.Bairro,
            NumeroCasa  = request.NumeroCasa
        };
        _repository.Cadastrar(novoPaciente);
        return novoPaciente;
    }
}
