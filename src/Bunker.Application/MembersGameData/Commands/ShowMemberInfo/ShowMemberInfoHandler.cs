using Bunker.Application.Abstractions;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.MembersGameData.Commands.ShowMemberInfo;

internal sealed class ShowMemberInfoHandler : ICommandHandler<ShowMemberInfoCommand, MemberInfo?>
{
  private readonly IRepository<MemberGameInfo> _membersGameInfoRepository;
  private readonly IUnitOfWork _unitOfWork;

  public ShowMemberInfoHandler(
    IRepository<MemberGameInfo> membersGameInfoRepository,
    IUnitOfWork unitOfWork)
  {
    _membersGameInfoRepository = membersGameInfoRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<MemberInfo?> Handle(ShowMemberInfoCommand request, CancellationToken cancellationToken)
  {
    var memberGameInfo = await _membersGameInfoRepository.TryFindAsync(gameInfo => gameInfo.Id == request.GameInfoId, cancellationToken, "Info");

    if (memberGameInfo is null)
    {
      return null;
    }

    memberGameInfo.Reveal();

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return memberGameInfo.Info;
  }
}