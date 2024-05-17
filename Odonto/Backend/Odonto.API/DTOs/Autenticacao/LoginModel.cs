using System.ComponentModel.DataAnnotations;

namespace Odonto.API.DTOs.Autenticacao;

public class LoginModel
{
    [Required(ErrorMessage = "É necessário informar o username!")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "É necessário informar a senha!")]
    public string? Password { get; set; }
}