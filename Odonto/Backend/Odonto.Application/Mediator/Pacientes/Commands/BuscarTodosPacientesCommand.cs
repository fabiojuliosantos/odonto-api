using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Pacientes.Commands;

public class BuscarTodosPacientesCommand: IRequest<IEnumerable<Paciente>>
{
}
