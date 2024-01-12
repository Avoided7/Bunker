using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class Disaster : Entity
{
  public string Name { get; private set; } = null!;

  private Disaster() { }

  public static Disaster Create(string name)
  {
    return new Disaster
    {
      Id = Guid.NewGuid().ToString(),
      Name = name
    };
  }
}