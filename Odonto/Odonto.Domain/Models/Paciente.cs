using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Odonto.Domain.Models;

public class Paciente
{
    public Paciente()
    {
        Consultas = new Collection<Consulta>();
    }

    public int PacienteId { get; set; }
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [StringLength(80)]
    public string Email { get; set; }
    [StringLength(11)]
    public string Telefone { get; set; }
    public ICollection<Consulta>? Consultas { get; set; }
}
