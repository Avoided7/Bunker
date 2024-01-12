using Bunker.Domain.Core;
using Bunker.Domain.Core.Enums;

namespace Bunker.Application.MembersGameData.Services;

public interface IMemberGameDataService
{
    MemberInfo GetRandomInfo(InfoType type);
    MemberGameData Generate(string memberId);
}