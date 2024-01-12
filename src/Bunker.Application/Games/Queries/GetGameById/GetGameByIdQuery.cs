using Bunker.Application.Abstractions;
using Bunker.Domain.Core;

namespace Bunker.Application.Games.Queries.GetGameById;

public sealed record GetGameByIdQuery(string Id) : IQuery<Game?>;