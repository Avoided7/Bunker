using Bunker.Application.Abstractions;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Members.Commands.DeleteMember;

internal sealed class DeleteMemberHandler : ICommandHandler<DeleteMemberCommand>
{
  private readonly IRepository<Member> _membersRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteMemberHandler(
    IRepository<Member> membersRepository,
    IUnitOfWork unitOfWork)
  {
    _membersRepository = membersRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
  {
    var member = await _membersRepository.TryFindAsync(member => member.Id == request.MemberId, cancellationToken);

    if (member is null)
    {
      return;
    }

    _membersRepository.Delete(member);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}