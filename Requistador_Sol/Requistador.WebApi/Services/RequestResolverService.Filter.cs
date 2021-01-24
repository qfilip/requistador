﻿using MediatR;
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
    public partial class RequestResolverService<T> where T : BaseEntity
    {
        private readonly IMediator _mediator;
        private readonly RequestDbContext _dbContext;
        public RequestResolverService(RequestDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
        }

        public async Task ResolveRequestsAsync(IEnumerable<AppRequest<T>> pendingRequests)
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
            _dbContext.InsertMany(castedRequests);
        }

        private async Task<IEnumerable<AppRequest<T>>> ResolveAddRequestsAsync(IEnumerable<AppRequest<T>> addRequests)
        {
            var systemRequests = new List<AppRequest<T>>();
            foreach(var request in addRequests)
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

        private async Task<IEnumerable<AppRequest<T>>> ResolveOtherRequestsAsync(IEnumerable<AppRequest<T>> otherRequests)
        {
            var systemRequests = new List<AppRequest<T>>();
            
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

            systemRequests.AddRange(await ResolveRequestsForProcessingAsync(requestsToProcess));
            systemRequests.AddRange(AutoRejectRequests(requestsForAutoReject));

            
            return systemRequests;
        }

        private async Task<IEnumerable<AppRequest<T>>> ResolveRequestsForProcessingAsync(IEnumerable<AppRequest<T>> clientRequests)
        {
            var systemRequests = new List<AppRequest<T>>();
            
            foreach(var request in clientRequests)
            {
                var resultStatus = eAppRequestStatus.None;
                
                if (request.Entity is Cocktail)
                    resultStatus = await ResolveCocktailRequestAsync(request);
                else if(request.Entity is Ingredient)
                    resultStatus = await ResolveIngredientRequestAsync(request);
                else if (request.Entity is Excerpt)
                    resultStatus = await ResolveExcerptRequestAsync(request);

                var systemRequest = CreateResultingRequest(request, resultStatus);
                systemRequests.Add(systemRequest);
            }


            return systemRequests;
        }

        private IEnumerable<AppRequest<T>> AutoRejectRequests(IEnumerable<AppRequest<T>> clientRequest)
        {
            var systemRequests = new List<AppRequest<T>>();

            foreach (var request in clientRequest)
                systemRequests.Add(CreateResultingRequest(request, eAppRequestStatus.Rejected));

            
            return systemRequests;
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