using Bunker.Application.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.AddToGame;

public sealed record AddToGameCommand(string Name, string GameId, string ConnectionId) : IQuery<Member?>;