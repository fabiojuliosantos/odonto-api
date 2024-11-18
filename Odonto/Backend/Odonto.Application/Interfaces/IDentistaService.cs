using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Application.Mediator.Dentistas.Queries;
using Odonto.Domain.Entities;

namespace Odonto.Application.Interfaces;

public interface IDentistaService
{
    Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync();
    Task<Dentista> BuscarPorIdAsync(BuscarDentistaPorIdCommand command);
    Task<Dentista> CadastrarDentista(CadastrarDentistaCommand command);
    Task<Dentista> AtualizarDentista(AtualizarDentistaCommand command);
    Task<Dentista> ExcluirDentista(ExcluirDentistaCommand command);
    Task<Dentista> BuscarDentistaEmail(BuscarDentistasEmailQuery query);

}
