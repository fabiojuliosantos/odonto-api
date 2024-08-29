using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Pacientes.Commands;

public class CadastrarPacienteCommand : IRequest<Paciente>
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public int NumeroCasa { get; set; }
}
