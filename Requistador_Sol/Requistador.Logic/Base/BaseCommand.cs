using MediatR;

namespace Requistador.Logic.Base
{
    public abstract class BaseCommand<TDto> : IRequest<TDto>
    {
    }
}
