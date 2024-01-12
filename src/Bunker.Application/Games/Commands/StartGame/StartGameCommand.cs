using Bunker.Application.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.StartGame;

public sealed record StartGameCommand(string GameId) : ICommand<IList<MemberGameData>?>;