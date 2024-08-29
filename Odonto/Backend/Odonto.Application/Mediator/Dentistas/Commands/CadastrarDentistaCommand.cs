using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Dentistas.Commands;

public class CadastrarDentistaCommand: IRequest<Dentista>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cro { get; set; }
}
