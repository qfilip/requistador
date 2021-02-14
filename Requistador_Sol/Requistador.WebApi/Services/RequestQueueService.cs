using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.AppConfiguration.Settings;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public partial class RequestQueueService : IHostedService
    {
        // TaskCreationOptions // check this
        private bool _locked;
        private Timer _timer;
        private readonly string _syslogPath;
        private readonly AppDbContext _dbContext;
        private readonly RequestDbContext _requestDbContext;
        RequestResolverService<BaseEntity> _resolverService;

        public RequestQueueService(
            IWebHostEnvironment environment,
            IServiceScopeFactory scopeFactory)
        {
            _dbContext = scopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            _requestDbContext = scopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RequestDbContext>();

            _resolverService = scopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RequestResolverService<BaseEntity>>();

            _syslogPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = AppSettings.GetProcessingInterval();
            var dueTime = TimeSpan.Zero;
            var period = TimeSpan.FromSeconds(interval);
            
            _timer = new Timer(ProcessRequests, null, dueTime, period);
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
                if(!_locked)
                {
                    _locked = true;
                    await _resolverService.ResolveAppRequestsAsync();
                    _locked = false;
                }
            };

            resolveAction();
        }
    }
}
