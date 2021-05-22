using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Requistador.WebApi.Controllers;
using System.Linq;

namespace Requistador.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseType = typeof(BaseApiController);
            var assembly = baseType.Assembly;

            var exc = typeof(BaseApiController).GetMethods().Select(x => x.Name);

            var ctrls = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).ToList();
            ctrls.ForEach(x =>
            {
                var ms = x.GetMethods().Where(a => !exc.Contains(a.Name)).ToList();
                ms.ForEach(m =>
                {
                    var attrs = m.GetCustomAttributes(true);
                    var rr = 0;
                });
                var xx = 0;
            });
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // webBuilder.UseUrls("http://localhost:5100");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
