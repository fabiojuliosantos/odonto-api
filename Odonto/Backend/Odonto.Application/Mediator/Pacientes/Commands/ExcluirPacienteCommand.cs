using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Pacientes.Commands;

public class ExcluirPacienteCommand : IRequest<Paciente>
{
    public int PacienteId { get; set; }
}
