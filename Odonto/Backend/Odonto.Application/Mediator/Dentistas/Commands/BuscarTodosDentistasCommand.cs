using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Dentistas.Commands;

public class BuscarTodosDentistasCommand : IRequest<IEnumerable<Dentista>>
{
}
