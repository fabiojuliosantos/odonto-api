using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Odonto.Infra.Context;
using Odonto.Infra.Interfaces;

namespace Odonto.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    #region Construtores e Membros

    public readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    #endregion Construtores e Membros
    
    #region Buscar
    
    public async Task<IEnumerable<T>> BuscarTodosAsync()
    {
        return await _context.Set<T>().AsNoTracking()
                                      .ToListAsync();
    }

    public async Task<T> BuscarPorIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking()
                                      .FirstOrDefaultAsync(predicate);
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