using Odonto.Domain.Entities;

namespace Odonto.Application.Interfaces;

public interface IDentistaService
{
    Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync();
    Task<Dentista> BuscarPorIdAsync(int id);
    Dentista CadastrarDentista(Dentista dentista);
    Dentista AtualizarDentista(Dentista dentista);
    Dentista ExcluirDentista(Dentista dentista);

}
