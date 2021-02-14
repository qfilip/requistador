using Microsoft.AspNetCore.Hosting;
using Requistador.Dtos;
using Requistador.Dtos.WebApi;
using Requistador.WebApi.ApiModels;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.AppConfiguration.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public class AdminService
    {
        private readonly string _service;
        private readonly string _syslogPath;
        private readonly SyslogService _syslogService;
        public AdminService(IWebHostEnvironment environment, SyslogService syslogService)
        {
            _syslogPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder);
            _syslogService = syslogService;
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

        public async Task<AppStateDto> GetLogFileAsync(string filename)
        {
            var path = Path.Combine(_syslogPath, filename);
            var bytes = await File.ReadAllBytesAsync(path);

            var result = new AppStateDto
            {
                LogFile = bytes
            };


            return result;
        }

        public async Task HandleRequestAsync(ApiAdminRequestDto dto)
        {

            if(dto.RequestFor == eAdminRequestFor.Timeout)
            {
                await HandleTimeoutChangeRequestAsync(dto);
            }
        }

        // handlers
        private async Task<bool> HandleTimeoutChangeRequestAsync(ApiAdminRequestDto dto)
        {
            if(dto.Args == null || dto.Args.Count == 0)
            {
                var subject = "Request processing timeout change error";
                var description = "Null or empty parameters provided";

                var log = CreateSyslog(subject, description, eLogSeverity.Worrying);
                await _syslogService.WriteLogAsync(log);

                return false;
            }

            var valid = Int32.TryParse(dto.Args[0], out int result);
            if(!valid)
            {
                var subject = "Request processing timeout change error";
                var description = "Parameter could not be casted to integer";

                var log = CreateSyslog(subject, description, eLogSeverity.Worrying);
                await _syslogService.WriteLogAsync(log);

                return false;
            }

            if(result < 1)
            {
                var subject = "Request processing timeout change error";
                var description = "Parameter value was less than 1";

                var log = CreateSyslog(subject, description, eLogSeverity.Worrying);
                await _syslogService.WriteLogAsync(log);

                return false;
            }


            AppSettings.SetRequestProcessingInterval(result);
            return true;
        }


        private Syslog CreateSyslog(string subject, string description, eLogSeverity severity)
        {
            return new Syslog(subject, description, severity, nameof(AdminService));
        }
    }
}
