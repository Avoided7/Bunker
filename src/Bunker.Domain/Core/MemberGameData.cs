using Bunker.Domain.Primitives;

namespace Bunker.Domain.Core;

public sealed class MemberGameData : Entity
{
  public MemberGameInfo Gender { get; private set; } = null!;
  public MemberGameInfo MentalHealth { get; private set; } = null!;
  public MemberGameInfo PhysicalHealth { get; private set; } = null!;
  public MemberGameInfo Talent { get; private set; } = null!;
  public MemberGameInfo ItemOne { get; private set; } = null!;
  public MemberGameInfo ItemTwo { get; private set; } = null!;
  public MemberGameInfo Work { get; private set; } = null!;
  public bool IsAlive { get; private set; }

  public string MemberId { get; private set; }

  private MemberGameData(string memberId)
  {
    MemberId = memberId;
  }

  public static MemberGameData Create(
    string memberId,
    MemberInfo gender,
    MemberInfo mentalHealth,
    MemberInfo physicalHealth,
    MemberInfo talent,
    MemberInfo itemOne,
    MemberInfo itemTwo,
    MemberInfo work)
  {
    return new MemberGameData(memberId)
    {
      Id = Guid.NewGuid().ToString(),
      Gender = MemberGameInfo.Create(gender, true),
      MentalHealth = MemberGameInfo.Create(mentalHealth),
      PhysicalHealth = MemberGameInfo.Create(physicalHealth),
      Talent = MemberGameInfo.Create(talent),
      ItemOne = MemberGameInfo.Create(itemOne),
      ItemTwo = MemberGameInfo.Create(itemTwo),
      Work = MemberGameInfo.Create(work),
      IsAlive = true
    };
  }
}

/*
 *
 *  Gender -> Male, Female, etc.
 *  Mental Health -> Very Bad, Bad, Medium, Good, Excellent
 *  Physical Health -> Very Bad, Bad, Medium, Good, Excellent
 *  Item #1 -> Empty, Disaster, Gun, Musical Instrument, etc.
 *  Item #2 -> Empty, Disaster, Gun, Musical Instrument, etc.
 *  Talent -> No talent, Can play instruments, etc.
 *  WorkAt -> Engineer, Programmer, Mathematical
 *  Special Item -> None,
 *
 */