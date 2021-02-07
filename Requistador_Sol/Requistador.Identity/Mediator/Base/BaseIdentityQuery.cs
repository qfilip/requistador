using MediatR;

namespace Requistador.Identity.Mediator.Base
{
    internal abstract class BaseIdentityQuery<TResponse> : IRequest<TResponse>
    {
    }
}
