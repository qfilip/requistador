using Microsoft.Extensions.DependencyInjection;
using Requistador.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

namespace Requistador.WebApi
{
    public static class WebApiServiceRegistry
    {
        public static void AddApiServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(MappingProfiles));
        }
    }
}
