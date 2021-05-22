using MapsterMapper;
using MediatR;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Dtos.Domain;
using Requistador.Logic.Commands.Request;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Base
{
    public abstract class BaseHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : IRequest<TResponse>
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;
        protected readonly AppDbContext _dbContext;
        protected readonly RequestDbContext _requestDbContext;

        private AppRequest _pendingRequest;

        public BaseHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public BaseHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BaseHandler(AppDbContext dbContext, IMediator mediator, IMapper mapper)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public BaseHandler(RequestDbContext requestDbContext)
        {
            _requestDbContext = requestDbContext;
        }

        public BaseHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseHandler(RequestDbContext requestDbContext, IMapper mapper)
        {
            _requestDbContext = requestDbContext;
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


        public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
        
        public async Task GeneratePendingRequest<TDto>(TDto dto, eAppRequestType requestType, eEntityTable table) where TDto : BaseDto
        {
            var requestDto = new AppRequestDto<TDto>
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,

                EntityStatus = eEntityStatus.Active,
                RequestStatus = eAppRequestStatus.Pending,
                RequestType = requestType,

                EntityId = dto.Id,
                DetailsObject = dto,
                EntityTable = table
            };
            _pendingRequest = MapToAppRequest(requestDto);
            TemporaryRequestStorage.AddRequest(_pendingRequest);

            await _mediator.Send(new AddRequestCommand(_pendingRequest));
        }
        public async Task<AppRequestDto<TDto>> GenerateResolvedRequest<TDto>() where TDto : BaseDto 
        {
            var resolvedRequest = new AppRequest
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                PendingRequestId = _pendingRequest.Id,

                EntityId = _pendingRequest.EntityId,
                EntityTable = _pendingRequest.EntityTable,
                RequestType = _pendingRequest.RequestType,
                RequestStatus = eAppRequestStatus.Resolved,
                DetailsJson = _pendingRequest.DetailsJson
            };

            await _mediator.Send(new AddRequestCommand(resolvedRequest));
            TemporaryRequestStorage.RemoveRequest(_pendingRequest);
            
            return MapFromResolvedToResult<TDto>(resolvedRequest);
        }
        
        private AppRequestDto<TDto> MapFromResolvedToResult<TDto>(AppRequest request) where TDto : BaseDto
        {
            return new AppRequestDto<TDto>
            {
                Id = request.Id,
                CreatedOn = request.CreatedOn,
                EntityId = request.EntityId,
                EntityTable = request.EntityTable,
                DetailsJson = request.DetailsJson,
                RequestType = request.RequestType,
                RequestStatus = request.RequestStatus,
                EntityStatus = request.EntityStatus
            };
        }
        private AppRequest MapToAppRequest<T>(AppRequestDto<T> dto) where T : BaseDto
        {
            return new AppRequest
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                PendingRequestId = dto.PendingRequestId,

                EntityId = dto.EntityId,
                EntityTable = dto.EntityTable,
                RequestType = dto.RequestType,
                RequestStatus = dto.RequestStatus,

                DetailsJson = dto.DetailsJson
            };
        }
    }
}
