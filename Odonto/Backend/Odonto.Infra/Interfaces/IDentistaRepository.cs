using Odonto.Domain.Entities;

namespace Odonto.Infra.Interfaces;

public interface IDentistaRepository 
{
    Task<IEnumerable<Dentista>> BuscarTodos();
    Task<Dentista> BuscarPorId(int id);
    Task<Dentista> BuscarPorEmail(string email);
    Task<Dentista> CadastrarNovo(Dentista dentista);
    Task<Dentista> AtualizarDentista(Dentista dentista);
    Task<Dentista> ExcluirDentista(int id);
}