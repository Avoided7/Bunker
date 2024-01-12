using Bunker.Application.Abstractions;
using Bunker.Application.Disasters.Services;
using Bunker.Application.MembersGameData.Services;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.StartGame;

internal sealed class StartGameHandler : ICommandHandler<StartGameCommand, IList<MemberGameData>?>
{
  private readonly IRepository<Game> _gamesRepository;
  private readonly IDisasterService _disasterService;
  private readonly IMemberGameDataService _memberGameDataService;
  private readonly IUnitOfWork _unitOfWork;

  public StartGameHandler(
    IRepository<Game> gamesRepository,
    IDisasterService disasterService,
    IMemberGameDataService memberGameDataService,
    IUnitOfWork unitOfWork)
  {
    _gamesRepository = gamesRepository;
    _disasterService = disasterService;
    _memberGameDataService = memberGameDataService;
    _unitOfWork = unitOfWork;
  }

  public async Task<IList<MemberGameData>?> Handle(StartGameCommand request, CancellationToken cancellationToken)
  {
    var game = await _gamesRepository.TryFindAsync(game => game.Id == request.GameId, cancellationToken, "Members.GameData");

    if (game is null)
    {
      return null;
    }

    if (game.Members.Count < Game.MIN_MEMBERS_COUNT)
    {
      return null;
    }

    var gameData = new List<MemberGameData>();
    var randomMap = _disasterService.GetRandomMap();
    
    game.StartGame(randomMap);

    foreach (var member in game.Members)
    {
      var memberGameData = _memberGameDataService.Generate(member.Id);

      member.SetGameData(memberGameData);

      gameData.Add(memberGameData);
    }

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return gameData;
  }
}