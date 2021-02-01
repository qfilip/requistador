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
            string appDbPath = GlobalVariables.AppDbSourcePrefix + environment.WebRootPath;

            var syslogsPath = Path.Combine(environment.WebRootPath, GlobalVariables.AppLogFolder);
            var dbConnString = Path.Combine(appDbPath, GlobalVariables.AppDbName);
            var requestDbConnString = Path.Combine(environment.WebRootPath, GlobalVariables.AppRequestDbName);

            _appSettings = new AppSettings(dbConnString, requestDbConnString);
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
            
            WebApiServiceRegistry.AddApiServices(services, _appSettings);
            
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == "http://localhost:4200")));

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
