using System.Linq.Expressions;

namespace Odonto.Infra.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> BuscarTodosAsync();
    Task<T> BuscarPorIdAsync(Expression<Func<T, bool>> predicate);
    T Cadastrar(T entity);
    T Atualizar(T entity);
    T Deletar(T entity);
}