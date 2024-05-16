using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

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

    #region Buscar

    public async Task<IEnumerable<T>> BuscarTodosAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> BuscarPorIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    #endregion Buscar
}