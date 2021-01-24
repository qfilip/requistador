using AutoMapper;
using MediatR;
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
        //protected readonly ApplicationDbContext _dbContext;
        //protected readonly CommandService _commandService;

        //public BaseHandler(ApplicationDbContext dbContext)
        //{
        //    _appMapper = new ManualMapper();
        //    _dbContext = dbContext;
        //}

        //public BaseHandler(ApplicationDbContext dbContext, IMediator mediator)
        //{
        //    _appMapper = new ManualMapper();
        //    _dbContext = dbContext;
        //    _mediator = mediator;
        //}

        //public BaseHandler(IMediator mediator, CommandService commandService, ApplicationDbContext dbContext)
        //{
        //    _appMapper = new ManualMapper();
        //    _mediator = mediator;
        //    _commandService = commandService;
        //    _dbContext = dbContext;
        //}

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
