using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odonto.API.Models;

public class Consulta
{
    public int ConsultaId { get; set; }

    [StringLength(100)] public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "DateTime")]
    public DateTime DataConsulta { get; set; }

    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
    public Dentista? Dentista { get; set; }
    public Paciente? Paciente { get; set; }
}