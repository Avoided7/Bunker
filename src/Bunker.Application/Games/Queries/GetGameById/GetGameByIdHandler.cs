using Bunker.Application.Abstractions;
using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Queries.GetGameById;

internal sealed class GetGameByIdHandler : IQueryHandler<GetGameByIdQuery, Game?>
{
  private readonly IRepository<Game> _gamesRepository;

  public GetGameByIdHandler(
    IRepository<Game> gamesRepository)
  {
    _gamesRepository = gamesRepository;
  }
  public Task<Game?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
  {
    return _gamesRepository.TryFindAsync(game => game.Id == request.Id,
      cancellationToken, 
      "Members.GameData.Gender.Info",
      "Members.GameData.MentalHealth.Info",
      "Members.GameData.PhysicalHealth.Info",
      "Members.GameData.ItemOne.Info",
      "Members.GameData.ItemTwo.Info",
      "Members.GameData.Work.Info",
      "Members.GameData.Talent.Info",
      "Disaster");
  }
}