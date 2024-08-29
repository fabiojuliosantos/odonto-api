using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Consultas.Commands;

public class BuscarConsultaPorIdCommand : IRequest<Consulta>
{
    public int ConsultaId { get; set; }
}
