using Bunker.Application.Abstractions;

namespace Bunker.Application.Members.Commands.DeleteMember;

public sealed record DeleteMemberCommand(string MemberId) : ICommand;