using Bunker.Domain.Core;

namespace Bunker.Application.Disasters.Services;

public interface IDisasterService
{
  Disaster GetRandomMap();
}