using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Dentistas.Commands;

public class AtualizarDentistaCommand : IRequest<Dentista>
{
    public int DentistaId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cro { get; set; }
}
