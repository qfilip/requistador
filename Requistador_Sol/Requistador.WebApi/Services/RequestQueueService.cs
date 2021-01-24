using Microsoft.Extensions.Hosting;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public class RequestQueueService : IHostedService
    {
        private Timer _timer;
        private readonly RequestDbContext _dbContext;
        private readonly RequestResolverService<BaseEntity> _resolverService;
        private readonly Expression<Func<AppRequest<BaseEntity>, bool>> _pendingQuery;

        public RequestQueueService(RequestDbContext dbContext, RequestResolverService<BaseEntity> resolverService)
        {
            _dbContext = dbContext;
            _resolverService = resolverService;
            _pendingQuery = (x) => x.RequestStatus == eAppRequestStatus.Pending;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProcessRequests, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private void ProcessRequests(object state)
        {
            Action resolveAction = async () =>
            {
                var pendingRequests = _dbContext.FindAll(_pendingQuery);
                await _resolverService.ResolveRequestsAsync(pendingRequests);
            };

            resolveAction();
        }
    }
}
