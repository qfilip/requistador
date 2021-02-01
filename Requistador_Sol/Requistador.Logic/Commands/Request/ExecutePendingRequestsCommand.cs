using MediatR;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Commands.Request
{
    public class ExecutePendingRequestsCommand : BaseCommand<int>
    {
        public ExecutePendingRequestsCommand()
        {

        }

        public class Handler : BaseHandler<ExecutePendingRequestsCommand, int>
        {
            private readonly Expression<Func<AppRequest<BaseEntity>, bool>> _pendingRequestsQueryExpr;
            public Handler(RequestDbContext requestDbContext, AppDbContext dbContext, IMediator mediator) : base(requestDbContext, dbContext, mediator)
            {
                _pendingRequestsQueryExpr = (x) => x.RequestStatus == eAppRequestStatus.Pending;
            }

            public override async Task<int> Handle(ExecutePendingRequestsCommand request, CancellationToken cancellationToken)
            {
                var pendingRequests = _requestDbContext.FindAll(_pendingRequestsQueryExpr);
                return await ResolveRequestsAsync(pendingRequests);
            }

            #region Filter
            public async Task<int> ResolveRequestsAsync<T>(IEnumerable<AppRequest<T>> pendingRequests) where T : BaseEntity
            {
                var systemRequests = new List<AppRequest<T>>();

                var addRequests = pendingRequests
                    .Where(x => x.RequestType == eAppRequestType.Add);
                var otherRequests = pendingRequests
                    .Where(x => x.RequestType != eAppRequestType.Add);

                systemRequests.AddRange(await ResolveAddRequestsAsync(addRequests));
                systemRequests.AddRange(await ResolveOtherRequestsAsync(otherRequests));

                systemRequests = systemRequests.OrderBy(x => x.CreatedOn).ToList();

                var castedRequests = systemRequests as IEnumerable<AppRequest<BaseEntity>>;

                _requestDbContext.InsertMany(castedRequests);
                var result = await _dbContext.SaveChangesAsync();

                return result;
            }

            private async Task<IEnumerable<AppRequest<T>>> ResolveAddRequestsAsync<T>(IEnumerable<AppRequest<T>> addRequests) where T : BaseEntity
            {
                var systemRequests = new List<AppRequest<T>>();
                foreach (var request in addRequests)
                {
                    var resultStatus = eAppRequestStatus.None;

                    if (request.Entity is Cocktail)
                        resultStatus = await ResolveCocktailRequestAsync(request);
                    else if (request.Entity is Ingredient)
                        resultStatus = await ResolveIngredientRequestAsync(request);
                    else if (request.Entity is Excerpt)
                        resultStatus = await ResolveExcerptRequestAsync(request);

                    var resultingRequest = CreateResultingRequest(request, resultStatus);
                    systemRequests.Add(resultingRequest);
                }

                return systemRequests;
            }

            private async Task<IEnumerable<AppRequest<T>>> ResolveOtherRequestsAsync<T>(IEnumerable<AppRequest<T>> otherRequests) where T : BaseEntity
            {
                var systemRequests = new List<AppRequest<T>>();

                var requestsToProcess = new List<AppRequest<T>>();
                var requestsForAutoReject = new List<AppRequest<T>>();

                var entityIds = otherRequests
                    .Select(x => x.Entity.Id)
                    .Distinct()
                    .ToList();

                foreach (var id in entityIds)
                {
                    var sameEntityRequests = otherRequests
                        .Where(x => x.Entity.Id == id)
                        .OrderBy(x => x.CreatedOn)
                        .ToList();

                    var editRequestsToProcess = sameEntityRequests.
                        TakeWhile(x => x.RequestType != eAppRequestType.Delete);

                    var deleteRequestToProcess = sameEntityRequests
                        .Find(x => x.RequestType == eAppRequestType.Delete);

                    var requestsToProcessIds = new List<Guid>();

                    if (editRequestsToProcess.Any())
                        requestsToProcessIds.AddRange(editRequestsToProcess.Select(x => x.Id));

                    if (deleteRequestToProcess != null)
                        requestsToProcessIds.Add(deleteRequestToProcess.Id);

                    var toProcess = sameEntityRequests
                        .Where(x => requestsToProcessIds.Contains(x.Id));

                    var toAutoReject = sameEntityRequests
                        .Where(x => !requestsToProcessIds.Contains(x.Id));

                    requestsToProcess.AddRange(toProcess);
                    requestsForAutoReject.AddRange(toAutoReject);
                }

                systemRequests.AddRange(await ResolveRequestsForProcessingAsync(requestsToProcess));
                systemRequests.AddRange(AutoRejectRequests(requestsForAutoReject));


                return systemRequests;
            }

            private async Task<IEnumerable<AppRequest<T>>> ResolveRequestsForProcessingAsync<T>(IEnumerable<AppRequest<T>> clientRequests) where T : BaseEntity
            {
                var systemRequests = new List<AppRequest<T>>();

                foreach (var request in clientRequests)
                {
                    var resultStatus = eAppRequestStatus.None;

                    if (request.Entity is Cocktail)
                        resultStatus = await ResolveCocktailRequestAsync(request);
                    else if (request.Entity is Ingredient)
                        resultStatus = await ResolveIngredientRequestAsync(request);
                    else if (request.Entity is Excerpt)
                        resultStatus = await ResolveExcerptRequestAsync(request);

                    var systemRequest = CreateResultingRequest(request, resultStatus);
                    systemRequests.Add(systemRequest);
                }


                return systemRequests;
            }

            private IEnumerable<AppRequest<T>> AutoRejectRequests<T>(IEnumerable<AppRequest<T>> clientRequest) where T : BaseEntity
            {
                var systemRequests = new List<AppRequest<T>>();

                foreach (var request in clientRequest)
                    systemRequests.Add(CreateResultingRequest(request, eAppRequestStatus.Rejected));


                return systemRequests;
            }

            private AppRequest<T> CreateResultingRequest<T>(AppRequest<T> clientRequest, eAppRequestStatus resultStatus) where T : BaseEntity
            {
                return new AppRequest<T>
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    PendingRequestId = clientRequest.Id,
                    RequestType = clientRequest.RequestType,
                    RequestStatus = resultStatus
                };
            }
            #endregion
            #region Entity
            private async Task<eAppRequestStatus> ResolveCocktailRequestAsync<T>(AppRequest<T> clientRequest) where T : BaseEntity
            {
                throw new NotImplementedException();
            }

            private async Task<eAppRequestStatus> ResolveIngredientRequestAsync<T>(AppRequest<T> clientRequest) where T : BaseEntity
            {
                var entity = clientRequest.Entity as Ingredient;
                var result = eAppRequestStatus.None;

                if (clientRequest.RequestType == eAppRequestType.Add)
                {
                    result = await _mediator.Send(new AddIngredientCommand(entity));
                }

                return result;
            }

            private async Task<eAppRequestStatus> ResolveExcerptRequestAsync<T>(AppRequest<T> clientRequest) where T : BaseEntity
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }
}
