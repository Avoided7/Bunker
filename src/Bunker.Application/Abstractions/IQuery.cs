using MediatR;

namespace Bunker.Application.Abstractions;

public interface IQuery<out TIn> : IRequest<TIn>
{
  
}