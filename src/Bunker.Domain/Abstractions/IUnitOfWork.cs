namespace Bunker.Domain.Abstractions;

public interface IUnitOfWork
{
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
  void SaveChanges();
}