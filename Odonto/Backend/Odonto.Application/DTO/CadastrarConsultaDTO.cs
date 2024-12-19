using Odonto.MessageBus;

namespace Odonto.Application.DTO;

public class CadastrarConsultaDTO : BaseMessage
{
    public string Descricao { get; set; }
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
}