using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Odonto.API.Models;

public class Consulta
{
    public int ConsultaId { get; set;}
    [StringLength(100)]
    public string Descricao { get; set; }
    [Required]
    [Column(TypeName="DateTime")]
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
    
    [JsonIgnore]
    public Dentista? Dentista { get; set; }
    [JsonIgnore]
    public Paciente? Paciente { get; set; }
}
