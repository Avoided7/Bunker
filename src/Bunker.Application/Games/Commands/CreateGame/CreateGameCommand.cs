using Bunker.Application.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Commands.CreateGame;

public sealed record CreateGameCommand(DateTime CreatedAt) : ICommand<Game>;