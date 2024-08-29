using MediatR;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Pacientes.Handlers;

public class CadastrarPacienteHandler : IRequestHandler<CadastrarPacienteCommand, Paciente>
{
    private readonly IPacienteRepository _repository;
    public CadastrarPacienteHandler(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Paciente> Handle(CadastrarPacienteCommand request, CancellationToken cancellationToken)
    {
        Paciente pacienteCadastro = new Paciente()
        {
            Nome = request.Nome,
            Cpf = request.Cpf,
            Telefone = request.Telefone,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            NumeroCasa = request.NumeroCasa,
            Email = request.Email
        };
        await _repository.CadastrarNovo(pacienteCadastro);
        return pacienteCadastro;
    }
}
