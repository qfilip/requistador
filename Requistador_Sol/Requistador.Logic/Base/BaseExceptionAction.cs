using MediatR.Pipeline;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Base
{
    public abstract class BaseRequestExceptionAction<TRequest, TException> : IRequestExceptionAction<TRequest, TException>
        where TException : Exception
    {
        private readonly RequestDbContext _requestDbContext;
        public BaseRequestExceptionAction(RequestDbContext requestDbContext)
        {
            _requestDbContext = requestDbContext;
        }

        public async Task Execute(TRequest request, TException exception, CancellationToken cancellationToken)
        {
            var failedRequests = TemporaryRequestStorage
                .GetAllRequests()
                .Select(x => MapToRejectedRequest(x));

            _requestDbContext.InsertMany(failedRequests);

            await Task.FromResult(true);
        }

        private AppRequest MapToRejectedRequest(AppRequest request)
        {
            return new AppRequest
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                PendingRequestId = request.Id,

                EntityId = request.EntityId,
                EntityTable = request.EntityTable,
                RequestType = request.RequestType,
                RequestStatus = eAppRequestStatus.Failed,
                DetailsJson = request.DetailsJson
            };
        }
    }
}
