using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Requistador.DataAccess.Contexts;
using Requistador.DataAccess.Extensions;
using Requistador.Domain.Base;
using Requistador.Logic.Base;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.Services;
using System.Reflection;

namespace Requistador.WebApi
{
    public static class WebApiServiceRegistry
    {
        public static void AddApiServices(IServiceCollection services, AppSettings appSettings)
        {
            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite(appSettings.DbConnectionString));
            services.AddRequestsDb(appSettings.LiteDbPath);
            
            services.AddHostedService<RequestQueueService>();
            services.AddTransient<RequestResolverService<BaseEntity>>();
            services.AddTransient<SyslogService>();
        }
    }
}
