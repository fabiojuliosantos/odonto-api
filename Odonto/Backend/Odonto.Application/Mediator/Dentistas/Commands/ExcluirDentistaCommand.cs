using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Dentistas.Commands;

public class ExcluirDentistaCommand : IRequest<Dentista>
{
    public int DentistaId { get; set; }
}
