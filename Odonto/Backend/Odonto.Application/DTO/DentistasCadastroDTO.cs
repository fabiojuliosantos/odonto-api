using Odonto.MessageBus;

namespace Odonto.API.DTO.Dentistas;

public class DentistasCadastroDTO : BaseMessage
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}