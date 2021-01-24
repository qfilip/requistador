using AutoMapper;
using MediatR;
using Requistador.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Base
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;
        protected readonly AppDbContext _dbContext;

        public BaseHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BaseHandler(AppDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
        }



        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
