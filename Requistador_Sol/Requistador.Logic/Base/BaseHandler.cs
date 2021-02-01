using AutoMapper;
using MediatR;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
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
        protected readonly RequestDbContext _requestDbContext;

        public BaseHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseHandler(RequestDbContext requestDbContext)
        {
            _requestDbContext = requestDbContext;
        }

        public BaseHandler(RequestDbContext requestDbContext, IMapper mapper)
        {
            _requestDbContext = requestDbContext;
            _mapper = mapper;
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

        public BaseHandler(RequestDbContext requestDbContext, AppDbContext dbContext, IMediator mediator)
        {
            _requestDbContext = requestDbContext;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        //public void MapDtoToAppRequest<T>(AppRequestDto<T> appRequestDto) where T : BaseDto
        //{
        //    if(appRequestDto.RequestType == eAppRequestType.Add)
        //    {
        //        appRequest.Id = Guid.NewGuid();
        //        appRequest.Entity.Id = Guid.NewGuid().ToString();
        //    }
        //}

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
