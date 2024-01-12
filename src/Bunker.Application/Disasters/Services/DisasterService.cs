using Bunker.Domain.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Disasters.Services;

internal sealed class DisasterService : IDisasterService
{
  private readonly IRepository<Disaster> _mapsRepository;

  public DisasterService(IRepository<Disaster> mapsRepository)
  {
    _mapsRepository = mapsRepository;
  }

  public Disaster GetRandomMap()
  {
    var maps = _mapsRepository.Get();
    var count = maps.Count();

    if (count == 0)
    {
      throw new InvalidOperationException("Cannot choose random map, if maps count is 0.");
    }

    var index = Random.Shared.Next(0, count);

    return maps.Skip(index).Take(1).First();
  }
}