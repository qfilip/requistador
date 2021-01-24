using Microsoft.Extensions.DependencyInjection;
using Requistador.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.DataAccess.Extensions
{
    // https://codehaks.github.io/2018/10/01/injecting-litedb-as-a-service-in-asp.net-core.html/
    public static class LiteDbServiceExtention
    {
        public static void AddRequestsDb(this IServiceCollection services, string databasePath)
        {
            services.AddTransient<RequestDbContext, RequestDbContext>();
            services.Configure<LiteDbConfig>(options => options.DatabasePath = databasePath);
        }
    }
}
