namespace Odonto.API.DTOs.Consultas;

public class ConsultasCadastroDTO
{
    public string Descricao { get; set; }
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
}