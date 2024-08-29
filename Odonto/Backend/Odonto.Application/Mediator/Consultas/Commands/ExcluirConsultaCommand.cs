using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Consultas.Commands;

public class ExcluirConsultaCommand : IRequest<Consulta>
{
    public int ConsultaId { get; set; }
}
