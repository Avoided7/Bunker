using MediatR;

namespace Bunker.Application.Abstractions;

public interface ICommandHandler<in TIn> : IRequestHandler<TIn>
  where TIn : IRequest
{

}

public interface ICommandHandler<in TIn, TOut> : IRequestHandler<TIn, TOut>
  where TIn : IRequest<TOut>
{

}