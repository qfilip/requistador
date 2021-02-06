using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Requistador.WebApi.AppConfiguration;

namespace Requistador.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(AppConstants.AppUrl);
                });
    }
}
