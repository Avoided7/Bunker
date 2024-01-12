namespace Bunker.Domain.Primitives;

public abstract class Entity : Entity<string>
{
  
}

public abstract class Entity<T>
{
  public T Id { get; set; } = default!;
}