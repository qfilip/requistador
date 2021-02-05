using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Requistador.DataAccess.Contexts;
using Requistador.DataAccess.Extensions;
using Requistador.Domain.Base;
using Requistador.Identity.Context;
using Requistador.Logic.Base;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.Services;
using System.Reflection;
using System.Text;

namespace Requistador.WebApi
{
    public static class ApiServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services, AppSettings appSettings)
        {
            ConfigureDatabases(services, appSettings);
            ConfigureAppServices(services);
            ConfigureImportedServices(services);

            // move to ConfigureSecurityServices() once auth is done
            ConfigureCors(services);
            // ConfigureAuthentication(services);
        }

        private static void ConfigureDatabases(IServiceCollection services, AppSettings appSettings)
        {
            services.AddRequestsDb(appSettings.RequestDbConnString);
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite(appSettings.AppDbConnString));
            services.AddDbContext<IdentityDbContext>(cfg => cfg.UseSqlite(appSettings.IdentityDbConnString));
        }

        private static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddHostedService<RequestQueueService>();
            services.AddTransient<RequestResolverService<BaseEntity>>();
            services.AddTransient<SyslogService>();
        }

        private static void ConfigureImportedServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);
            //services.AddAutoMapper(typeof(MappingProfiles));
            
            services.AddSingleton(AppSettings.GetMapsterConfiguration());
            services.AddTransient<IMapper, ServiceMapper>();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == AppConstants.Client_AllowedOrigin)));
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            // #r nuget: Microsoft.AspNetCore.Authentication.JwtBearer
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = AppConstants.Auth_ValidIssuer,
                    ValidAudience = AppConstants.Auth_ValidAudience,
                    IssuerSigningKey = AppSettings.GetAppKey()
                };
            });
        }
    }
}
