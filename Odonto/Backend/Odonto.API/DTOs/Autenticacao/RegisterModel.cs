using System.ComponentModel.DataAnnotations;

namespace Odonto.API.DTOs.Autenticacao;

public class RegisterModel
{
    [Required(ErrorMessage = "É necessário informar o username!")]
    public string? Username { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "É necessário informar o e-mail!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "É necessário informar a senha!")]
    public string? Password { get; set; }
    [Required(ErrorMessage ="É necessário informar o nome!")]
    public string Nome { get; set; }
}