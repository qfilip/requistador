using MapsterMapper;
using MediatR;
using Requistador.Identity.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Identity.Mediator.Base
{
    internal abstract class BaseIdentityHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMapper _mapper;
        protected readonly IdentityDbContext _identityDbContext;

        public BaseIdentityHandler(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public BaseIdentityHandler(IdentityDbContext identityDbContext, IMapper mapper)
        {
            _identityDbContext = identityDbContext;
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
