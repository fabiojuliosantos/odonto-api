namespace Odonto.API.DTOs.Pacientes;

public class PacientesCadastroDTO
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
}