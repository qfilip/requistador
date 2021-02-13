using Microsoft.AspNetCore.Hosting;
using Requistador.Dtos.WebApi;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.AppConfiguration.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public class AppStateService
    {
        private readonly string _syslogPath;
        public AppStateService(IWebHostEnvironment environment)
        {
            _syslogPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder);
        }

        public AppStateDto GetAppSettings()
        {
            var syslogFiles = Directory.EnumerateFiles(_syslogPath);
            var files = new List<string>();
            foreach(var file in syslogFiles)
            {
                var filename = Path.GetFileName(file);
                files.Add(filename);
            }

            var dto = new AppStateDto
            {
                ProcessingInterval = AppSettings.GetProcessingInterval(),
                SyslogFiles = files
            };

            return dto;
        }

        public AppStateDto SetProcessingInterval(AppStateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Request dto is null");
            }
            else if (dto.ProcessingInterval < 1)
            {
                throw new ArgumentException("Processing interval minimum value is 1");
            }
            else
            {
                AppSettings.SetRequestProcessingInterval(dto.ProcessingInterval);
            }

            var result = new AppStateDto
            {
                ProcessingInterval = AppSettings.GetProcessingInterval()
            };

            
            return result;
        }


        public async Task<AppStateDto> GetLogFile(string filename)
        {
            var path = Path.Combine(_syslogPath, filename);
            var bytes = await File.ReadAllBytesAsync(path);

            var result = new AppStateDto
            {
                LogFile = bytes
            };


            return result;
        } 
    }
}
