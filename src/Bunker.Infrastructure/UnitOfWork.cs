using Bunker.Domain.Abstractions;

namespace Bunker.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _dbContext;

  public UnitOfWork(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public Task SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return _dbContext.SaveChangesAsync(cancellationToken);
  }

  public void SaveChanges()
  {
    _dbContext.SaveChanges();
  }
}