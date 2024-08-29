using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Consultas.Commands;

public class BuscarTodasConsultasCommand : IRequest<IEnumerable<Consulta>>
{
}
