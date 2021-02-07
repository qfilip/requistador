using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Identity.Mediator.Base
{
    internal abstract class BaseIdentityCommand<TDto> : IRequest<TDto>
    {
    }
}
