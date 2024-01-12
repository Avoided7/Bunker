using Bunker.Application.Abstractions;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.AddToGame;

internal sealed class AddToGameHandler : ICommandHandler<AddToGameCommand, Member?>
{
  private readonly IRepository<Game> _gamesRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddToGameHandler(
    IRepository<Game> gamesRepository,
    IUnitOfWork unitOfWork)
  {
    _gamesRepository = gamesRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Member?> Handle(AddToGameCommand request, CancellationToken cancellationToken)
  {
    var game = await _gamesRepository.TryFindAsync(game => game.Id == request.GameId, cancellationToken, "Members");

    if (game is null)
    {
      return null;
    }

    var member = Member.Create(request.Name, request.GameId, request.ConnectionId, game.Members.Count == 0);

    game.Members.Add(member);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return member;
  }
}