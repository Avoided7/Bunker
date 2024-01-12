using System.Linq.Expressions;

namespace Bunker.Domain.Abstractions;

public interface IRepository<T>
  where T : class
{
  IQueryable<T> Get();
  IQueryable<T> Get(Expression<Func<T, bool>> expression);

  Task<T?> TryFindAsync(
    Expression<Func<T, bool>> expression,
    CancellationToken cancellationToken = default,
    params string[] includes);

  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}