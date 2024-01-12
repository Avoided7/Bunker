using Bunker.Application.Abstractions;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.CreateGame;

internal sealed class CreateGameHandler : ICommandHandler<CreateGameCommand, Game>
{
  private readonly IRepository<Game> _gamesRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateGameHandler(
    IRepository<Game> gamesRepository,
    IUnitOfWork unitOfWork)
  {
    _gamesRepository = gamesRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
  {
    var game = Game.Create(request.CreatedAt);

    _gamesRepository.Create(game);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return game;
  }
}