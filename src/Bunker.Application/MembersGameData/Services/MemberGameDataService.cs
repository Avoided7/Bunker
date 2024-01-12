using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;
using Bunker.Domain.Core.Enums;

namespace Bunker.Application.MembersGameData.Services;

internal sealed class MemberGameDataService : IMemberGameDataService
{
    private readonly IRepository<MemberInfo> _membersInfoRepository;

    public MemberGameDataService(IRepository<MemberInfo> membersInfoRepository)
    {
        _membersInfoRepository = membersInfoRepository;
    }

    public MemberInfo GetRandomInfo(InfoType type)
    {
        var members = _membersInfoRepository.Get(member => member.Type == type);
        var count = members.Count();

        if (count == 0)
        {
            throw new ArgumentException("A Members info count with this type equal zero.");
        }

        var index = Random.Shared.Next(0, count);

        return members.Skip(index).Take(1).First();
    }

    public MemberGameData Generate(string memberId)
    {
      var itemOne = GetRandomInfo(InfoType.Item);
      var itemTwo = GetRandomInfo(InfoType.Item);
      var gender = GetRandomInfo(InfoType.Gender);
      var mentalHealth = GetRandomInfo(InfoType.MentalHealth);
      var physicalHealth = GetRandomInfo(InfoType.PhysicalHeath);
      var talent = GetRandomInfo(InfoType.Talent);
      var work = GetRandomInfo(InfoType.Work);

      var gameData = MemberGameData.Create(memberId, gender, mentalHealth, physicalHealth, talent, itemOne, itemTwo, work);

      return gameData;
    }
}