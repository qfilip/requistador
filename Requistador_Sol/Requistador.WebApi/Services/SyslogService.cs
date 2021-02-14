using Microsoft.AspNetCore.Hosting;
using Requistador.WebApi.ApiModels;
using Requistador.WebApi.AppConfiguration;
using System;
using System.IO;
using System.Threading.Tasks;

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


        public async Task WriteLogAsync(Syslog log)
        {
            var file = CreateFile(log.CreatedOn, log.CreatedBy);
            var lines = log.GetLines();

            await File.WriteAllLinesAsync(file, lines);
        }


        private string CreateFile(string createdAt, string from)
        {
            var filename = $"{createdAt}_{from}.txt";
            return Path.Combine(_syslogPath, filename);
        }
    }
}
