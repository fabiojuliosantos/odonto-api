using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Repositories.Interface;
using System.Linq.Expressions;

namespace Odonto.API.Repositories.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    #region Buscar
    public IEnumerable<T> BuscarTodos()
    {
        return _context.Set<T>().ToList();
     
    }
    public T BuscarPorId(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    #endregion Buscar

    #region Cadastrar
    public T Cadastrar(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }
    #endregion Cadastrar

    #region Atualizar
    public T Atualizar(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;

    }
    #endregion Atualizar

    #region Deletar
    public T Deletar(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
    #endregion Deletar

}
