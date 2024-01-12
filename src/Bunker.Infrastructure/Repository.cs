using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Bunker.Domain.Abstractions;

namespace Bunker.Infrastructure;

internal sealed class Repository<T> : IRepository<T>
  where T : class
{
  private readonly DbSet<T> _set;

  public Repository(ApplicationDbContext dbContext)
  {
    _set = dbContext.Set<T>();
  }

  public IQueryable<T> Get()
  {
    return _set;
  }

  public IQueryable<T> Get(Expression<Func<T, bool>> expression)
  {
    return _set.Where(expression);
  }

  public Task<T?> TryFindAsync(
    Expression<Func<T, bool>> expression, 
    CancellationToken cancellationToken = default,
    params string[] includes)
  {
    IQueryable<T> included = _set;

    foreach (var include in includes)
    {
      included = included.Include(include);
    }

    return included.FirstOrDefaultAsync(expression, cancellationToken);
  }

  public void Create(T entity)
  {
    _set.Entry(entity).State = EntityState.Added;
  }

  public void Update(T entity)
  {
    _set.Entry(entity).State = EntityState.Modified;
  }

  public void Delete(T entity)
  {
    _set.Entry(entity).State = EntityState.Deleted;
  }
}