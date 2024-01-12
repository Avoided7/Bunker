using Bunker.Domain.Core.Enums;
using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class Game : Entity
{
  public const int MIN_MEMBERS_COUNT = 1;

  public IList<Member> Members { get; private set; } = null!;
  public GameState State { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public Disaster? Disaster { get; private set; }

  private Game() { }

  public static Game Create(
    DateTime createdAt,
    GameState gameState = GameState.Default,
    IEnumerable<Member>? members = null)
  {
    return new Game
    {
      Id = Guid.NewGuid().ToString(),
      CreatedAt = createdAt,
      State = gameState,
      Members = new List<Member>(members ?? Enumerable.Empty<Member>())
    };
  }

  public void StartGame(Disaster disaster)
  {
    if (State != GameState.Default)
    {
      throw new InvalidOperationException("Cannot start already started/ended game.");
    }

    if (MIN_MEMBERS_COUNT > Members.Count)
    {
      throw new InvalidOperationException("Cannot start a game with a small number of people.");
    }

    State = GameState.InProcess;
    Disaster = disaster;
  }

  public void EndGame()
  {
    if (State != GameState.InProcess)
    {
      throw new InvalidOperationException("Cannot stop not started game.");
    }

    State = GameState.Ended;
  }

  public void RestartGame()
  {
    State = GameState.Default;
  }
}