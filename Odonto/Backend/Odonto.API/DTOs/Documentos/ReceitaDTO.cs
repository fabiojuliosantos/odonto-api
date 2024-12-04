namespace Odonto.API.DTOs.Documentos;

public class ReceitaDTO
{
    public int PacienteId { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
}
