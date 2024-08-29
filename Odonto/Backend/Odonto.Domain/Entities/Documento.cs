namespace Odonto.Domain.Entities;

public class Documento
{
    public int DocumentoId { get; set; }
    public string TipoDocumento { get; set; }
    public DateTime DataEmissao { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
    public string NomeDocumento { get; set; }
    public Paciente? Paciente { get; set; }
    public Dentista? Dentista { get; set; }
}
