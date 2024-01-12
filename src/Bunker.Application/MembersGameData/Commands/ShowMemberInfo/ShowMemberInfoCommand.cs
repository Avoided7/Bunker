using Bunker.Application.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.MembersGameData.Commands.ShowMemberInfo;

public sealed record ShowMemberInfoCommand(string MemberId, string GameInfoId) : ICommand<MemberInfo?>;