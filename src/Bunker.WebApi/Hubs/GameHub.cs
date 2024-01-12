using System.Collections.Concurrent;
using Bunker.Application.Games.Commands.AddToGame;
using Bunker.Application.Games.Commands.StartGame;
using Bunker.Application.Members.Commands.DeleteMember;
using Bunker.Application.MembersGameData.Commands.ShowMemberInfo;
using Bunker.Domain.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bunker.WebApi.Hubs;

internal sealed class GameHub : Hub<IGameHub>
{
  private readonly IMediator _mediator;
  private static readonly ConcurrentDictionary<string, Member> Members = new ();
  private static readonly ConcurrentDictionary<string, string> Connections = new ();

  public GameHub(IMediator mediator)
  {
    _mediator = mediator;
  }

  #region SignalR Methods

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    var connectionId = Context.ConnectionId;

    if (!Members.TryGetValue(connectionId, out var member))
    {
      return;
    }

    var gameId = member.GameId;
    var deleteMemberCommand = new DeleteMemberCommand(member.Id);

    await _mediator.Send(deleteMemberCommand);

    await Groups.RemoveFromGroupAsync(connectionId, gameId);
    await Clients.Group(gameId).UserDisconnected(member.Id);

    Members.TryRemove(connectionId, out _);
    Connections.TryRemove(member.Id, out _);

    await base.OnDisconnectedAsync(exception);
  }

  #endregion

  #region Invokable Methods

  public async Task Connect(string name, string gameId)
  {
    var connectionId = Context.ConnectionId;
    var command = new AddToGameCommand(name, gameId, connectionId);

    var member = await _mediator.Send(command);

    if (member is null)
    {
      return;
    }

    Members.TryAdd(connectionId, member);
    Connections.TryAdd(member.Id, connectionId);

    await Groups.AddToGroupAsync(connectionId, gameId);

    await Clients.OthersInGroup(gameId).UserConnected(member);
    await Clients.Caller.Connected(member);
  }

  public async Task StartGame()
  {
    if (!Members.TryGetValue(Context.ConnectionId, out var member))
    {
      return;
    }

    var gameId = member.GameId;

    var startGameCommand = new StartGameCommand(gameId);

    var gameData = await _mediator.Send(startGameCommand);

    if (gameData is null)
    {
      return;
    }

    var tasks = new List<Task>();

    foreach (var memberGameData in gameData)
    {
      var client = Connections[memberGameData.MemberId];

      tasks.Add(Clients.Client(client).GameStarted(memberGameData));
    }

    await Task.WhenAll(tasks);
  }

  public async Task RevealInfo(string gameInfoId)
  {
    if (!Members.TryGetValue(Context.ConnectionId, out var member))
    {
      return;
    }

    var showMemberInfoCommand = new ShowMemberInfoCommand(member.Id, gameInfoId);

    var memberInfo = await _mediator.Send(showMemberInfoCommand);

    if (memberInfo is null)
    {
      return;
    }

    await Clients.OthersInGroup(member.GameId).UserRevealedInfo(member.Id, memberInfo);
  }

  #endregion

}

public interface IGameHub
{
  Task Connected(Member member);
  Task UserConnected(Member member);
  Task UserDisconnected(string memberId);
  Task GameStarted(MemberGameData gameData);
  Task UserRevealedInfo(string memberId, MemberInfo info);
}