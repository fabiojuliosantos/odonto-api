using Odonto.MessageBus;

namespace Odonto.API.DTOs.Documentos;

public class ReceitaDTO : BaseMessage
{
    public string? Usuario { get; set; }
    public int PacienteId { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
}

public class Medicamento
{
    public string Nome { get; set; }
    public string Dose { get; set; }
    public string Posologia { get; set; }
    public string Observacoes { get; set; }
}
