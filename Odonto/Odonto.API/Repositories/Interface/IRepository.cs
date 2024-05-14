using System.Linq.Expressions;

namespace Odonto.API.Repositories.Interface;

public interface IRepository<T> 
{
    IEnumerable<T> BuscarTodos();
    T BuscarPorId (Expression<Func<T, bool>> predicate);
    T Cadastrar(T entity);
    T Atualizar(T entity);
    T Deletar(T entity);
}
