using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Requistador.WebApi.AppConfiguration;
using System.IO;
using System.Reflection;

namespace Requistador.WebApi
{
    public class Startup
    {
        private readonly AppSettings _appSettings;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            string appDbPath = AppConstants.AppDbSourcePrefix + environment.WebRootPath;

            var syslogsPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder);
            var appDbConnString = Path.Combine(appDbPath, AppConstants.AppDbName);
            var requestDbConnString = Path.Combine(environment.WebRootPath, AppConstants.AppRequestDbName);
            var identityDbConnString = Path.Combine(environment.WebRootPath, AppConstants.AppIdentityDbName); ;

            _appSettings = new AppSettings(appDbConnString, requestDbConnString, identityDbConnString);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // #r nuget: Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(o => {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;
            
            ApiServiceRegistry.RegisterServices(services, _appSettings);

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Requistador.WebApi", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Requistador.WebApi v1"));
            }

            app.UseRouting();
            app.UseCors();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });
        }
    }
}
