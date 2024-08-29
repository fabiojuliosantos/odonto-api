using System.Collections.ObjectModel;

namespace Odonto.Domain.Entities;

public class Paciente
{
    public Paciente()
    {
        Consultas = new Collection<Consulta>();
    }

    public int PacienteId { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public int NumeroCasa { get; set; }
    public IEnumerable<Consulta>? Consultas { get; set; }
    public IEnumerable<Documento>? Documentos { get; set; }
}