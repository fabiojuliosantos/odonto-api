using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Odonto.API.Models;

public class Dentista
{
    public Dentista()
    {
        Consultas = new Collection<Consulta>();
    }
    public int DentistaId { get; set; }
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }
    [StringLength(80)]
    public string Email { get; set; }
    [StringLength(11)]
    public string Telefone { get; set; }
    public ICollection<Consulta>? Consultas { get; set; }
}
