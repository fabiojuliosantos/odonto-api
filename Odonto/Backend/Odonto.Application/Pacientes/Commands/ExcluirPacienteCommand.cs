using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Pacientes.Commands;

public class ExcluirPacienteCommand : IRequest<Paciente>
{
    public int PacienteId { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}
