using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Odonto.Domain.Entities;

public class Dentista
{
    public int DentistaId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cro { get; set; }
    public IEnumerable<Consulta>? Consultas { get; set; }
    public IEnumerable<Documento>? Documentos { get; set; }

}