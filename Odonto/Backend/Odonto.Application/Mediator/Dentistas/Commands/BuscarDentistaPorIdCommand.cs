using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Dentistas.Commands;

public class BuscarDentistaPorIdCommand: IRequest<Dentista>
{
    public int DentistaId { get; set; }
}
