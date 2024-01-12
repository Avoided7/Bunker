using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class MemberGameInfo : Entity
{
  public MemberInfo Info { get; private set; } = null!;
  public bool IsRevealed { get; private set; } = false;

  private MemberGameInfo() { }

  public static MemberGameInfo Create(MemberInfo memberInfo, bool isRevealed = false)
  {
    return new MemberGameInfo
    {
      Id = Guid.NewGuid().ToString(),
      Info = memberInfo,
      IsRevealed = isRevealed
    };
  }

  public void Reveal()
  {
    IsRevealed = true;
  }
}