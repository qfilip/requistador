using Microsoft.AspNetCore.Hosting;
using Requistador.WebApi.AppConfiguration;
using System;
using System.IO;

namespace Requistador.WebApi.Services
{
    public class SyslogService
    {
        private readonly string _syslogPath;
        public SyslogService(IWebHostEnvironment environment)
        {
            _syslogPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder);
        }

        public void CreateTestLog()
        {
            var filename = $"{DateTime.UtcNow.ToString(AppConstants.Format_SyslogTime)}_Test.txt";
            var message = "Test log successful";
            var filepath = Path.Combine(_syslogPath, filename);

            File.WriteAllText(filepath, message);
        }
    }
}
