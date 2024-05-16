using Odonto.API.Models;

namespace Odonto.API.Services.Interface;

public interface IDentistaService
{
    Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync();
    Task<Dentista> BuscarPorIdAsync(int id);
    Dentista CadastrarDentista(Dentista dentista);
    Dentista AtualizarDentista(Dentista dentista);
    Dentista ExcluirDentista(Dentista dentista);
}