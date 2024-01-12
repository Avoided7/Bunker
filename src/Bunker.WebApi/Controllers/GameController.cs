using Bunker.Application.Games.Commands.CreateGame;
using Bunker.Application.Games.Queries.GetGameById;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Bunker.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GameController : ControllerBase
{
  private readonly IMediator _mediator;

  public GameController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateGame(CancellationToken cancellationToken)
  {
    var command = new CreateGameCommand(DateTime.UtcNow);

    var game = await _mediator.Send(command, cancellationToken);

    return Ok(game);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetGameById(string id, CancellationToken cancellationToken)
  {
    var query = new GetGameByIdQuery(id);

    var game = await _mediator.Send(query, cancellationToken);

    return game is null ? NotFound() : Ok(game);
  }
}