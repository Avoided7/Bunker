using Bunker.Domain.Core.Enums;
using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class MemberInfo : Entity
{
  public string Value { get; private set; } = null!;
  public InfoType Type { get; private set; } = InfoType.None;

  private MemberInfo() { }

  public static MemberInfo Create(
    InfoType type,
    string name)
  {
    if (type == InfoType.None)
    {
      throw new ArgumentException(nameof(type));
    }

    return new MemberInfo
    {
      Id = Guid.NewGuid().ToString(),
      Value = name,
      Type = type
    };
  }
}