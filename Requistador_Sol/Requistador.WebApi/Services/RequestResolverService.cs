using MediatR;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Logic.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public class RequestResolverService<T> where T : BaseEntity
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _dbContext;
        public RequestResolverService(AppDbContext dbContext, IMediator mediator)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task ResolveRequests(IEnumerable<AppRequest<T>> pendingRequests)
        {
            var systemRequests = new List<AppRequest<T>>();
            
            var addRequests = pendingRequests
                .Where(x => x.RequestType == eAppRequestType.Add);
            var otherRequests = pendingRequests
                .Where(x => x.RequestType != eAppRequestType.Add);

            systemRequests.AddRange(await ResolveAddRequests(addRequests));
            systemRequests.AddRange(await ResolveOtherRequests(otherRequests));
        }

        private async Task<IEnumerable<AppRequest<T>>> ResolveAddRequests(IEnumerable<AppRequest<T>> addRequests)
        {
            var systemRequests = new List<AppRequest<T>>();
            foreach(var request in addRequests)
            {
                var resultStatus = eAppRequestStatus.None;
                if (request.Entity is Cocktail)
                {
                    var entity = request.Entity as Cocktail;
                    resultStatus = await _mediator.Send(new AddCocktailCommand(entity));
                }
                //else if (request.Entity is IngredientDto)
                //{
                //    var dto = request.Entity as IngredientDto;
                //    await _mediator.Send(new AddCocktailCommand(dto));
                //}
                //else if (request.Entity is ExcerptDto)
                //{
                //    var dto = request.Entity as ExcerptDto;
                //    await _mediator.Send(new AddCocktailCommand(dto));
                //}
                var resultingRequest = CreateResultingRequest(request, resultStatus);
                systemRequests.Add(resultingRequest);
            }

            return systemRequests;
        }

        private async Task<IEnumerable<AppRequest<T>>> ResolveOtherRequests(IEnumerable<AppRequest<T>> otherRequests)
        {
            var requestsToProcess = new List<AppRequest<T>>();
            var requestsForAutoReject = new List<AppRequest<T>>();

            var entityIds = otherRequests
                .Select(x => x.Entity.Id)
                .Distinct()
                .ToList();

            foreach(var id in entityIds)
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
                
                if(editRequestsToProcess.Any())
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
        }

        private AppRequest<T> CreateResultingRequest(AppRequest<T> clientRequest, eAppRequestStatus resultStatus)
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
    }
}
