using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class Member : Entity
{
  public string Name { get; private set; } = null!;
  public string GameId { get; private set; } = null!;
  public string ConnectionId { get; private set; } = null!;
  public bool IsLeader { get; private set; }

  public MemberGameData? GameData { get; private set; }

  private Member() { }

  public static Member Create(
    string name,
    string gameId,
    string connectionId,
    bool isLeader)
  {
    return new Member
    {
      Id = Guid.NewGuid().ToString(),
      Name = name,
      GameId = gameId,
      ConnectionId = connectionId,
      IsLeader = isLeader
    };
  }

  public void SetGameData(MemberGameData gameData)
  {
    ArgumentNullException.ThrowIfNull(gameData);

    GameData = gameData;
  }
}

