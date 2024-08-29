using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Consultas.Commands;

public class CadastrarConsultaCommand : IRequest<Consulta>
{
    public string Descricao { get; set; }
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
}
