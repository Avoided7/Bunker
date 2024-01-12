using MediatR;

namespace Bunker.Application.Abstractions;

public interface ICommand : IRequest
{

}

public interface ICommand<out T> : IRequest<T>
{
  
}