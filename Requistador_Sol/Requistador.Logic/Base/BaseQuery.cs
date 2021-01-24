using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Logic.Base
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
    }
}
