using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Consultas.Commands;

public class AtualizarConsultaCommand : IRequest<Consulta>
{
    public int ConsultaId { get; set; }
    public string Descricao { get; set; }
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
}
