using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Requistador.DataAccess.Contexts;
using Requistador.DataAccess.Extensions;
using Requistador.Logic.Base;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.Services;
using System.Reflection;

namespace Requistador.WebApi
{
    public static class WebApiServiceRegistry
    {
        public static void AddApiServices(IServiceCollection services, string appDbPath, string requestsDbPath)
        {
            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite(appDbPath));
            services.AddRequestsDb(requestsDbPath);
            services.AddHostedService<RequestQueueService>();
        }
    }
}
