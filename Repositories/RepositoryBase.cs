using System.Linq.Expressions;
using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected InvenioContext Context { get; set; }
    
    public RepositoryBase(InvenioContext context)
    {
        Context = context;
    }
    
    public IQueryable<T> FindAll() => 
        Context.Set<T>().AsNoTracking();
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
        Context.Set<T>().Where(expression).AsNoTracking();
    
    public void Create(T entity) =>
        Context.Set<T>().Add(entity);
    
    public void Update(T entity) =>
        Context.Set<T>().Update(entity);
    
    public void Delete(T entity) =>
        Context.Set<T>().Remove(entity);
}