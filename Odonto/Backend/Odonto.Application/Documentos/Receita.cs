namespace Odonto.Application.Documentos;

public class Receita
{
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
