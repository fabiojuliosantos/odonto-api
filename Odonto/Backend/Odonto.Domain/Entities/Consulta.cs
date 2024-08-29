using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odonto.Domain.Entities;

public class Consulta
{
    public int ConsultaId { get; set; }
    public string Descricao { get; set; }
    public DateTime DataConsulta { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    public Dentista? Dentista { get; set; }
}