using Odonto.API.Models;

namespace Odonto.API.Services.Interface;

public interface IDentistaService
{
    IEnumerable<Dentista> BuscarTodosDentistas();
    Dentista BuscarPorId(int id);
    Dentista CadastrarDentista(Dentista dentista);
    Dentista AtualizarDentista(Dentista dentista);
    Dentista ExcluirDentista(Dentista dentista);
}
