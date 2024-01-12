using MediatR;

namespace Bunker.Application.Abstractions;

public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut>
  where TIn : IRequest<TOut>
{

}