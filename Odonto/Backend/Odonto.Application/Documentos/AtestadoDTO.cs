using Odonto.MessageBus;

namespace Odonto.API.DTOs.Documentos;

public class AtestadoDTO : BaseMessage
{
    public int pacienteId { get; set; }
    public string cid10 { get; set; }
    public string? Usuario { get; set; }
    public int quantidadeDias { get; set; }

}
