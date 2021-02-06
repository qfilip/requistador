using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Requistador.WebApi.AppConfiguration.Settings
{
    public sealed partial class AppSettings
    {
        private class AppSettingsParameters
        {
            public string SyslogPath { get; init; }
            public string AppDbConnString { get; init; }
            public string RequestDbConnString { get; init; }
            public string IdentityDbConnString { get; init; }
        }
        
        private static AppSettings _instance;
        private readonly AppSettingsParameters _parameters;

        // private static readonly object _lock = new object();

        public AppSettings(IWebHostEnvironment environment)
        {
            string appDbPath = AppConstants.AppDbSourcePrefix + environment.WebRootPath;
            var parameters = new AppSettingsParameters
            {
                SyslogPath = Path.Combine(environment.WebRootPath, AppConstants.AppLogFolder),
                AppDbConnString = Path.Combine(appDbPath, AppConstants.AppDbName),
                RequestDbConnString = Path.Combine(environment.WebRootPath, AppConstants.AppRequestDbName),
                IdentityDbConnString = Path.Combine(environment.WebRootPath, AppConstants.AppIdentityDbName)
            };

            _instance = new AppSettings(parameters);
        }

        private AppSettings(AppSettingsParameters parameters)
        {
            _parameters = parameters;
        }

        private static AppSettings Instance
        {
            get
            {
                if (_instance != null) return _instance;
                else
                {
                    Environment.FailFast("Application settings not initiated");
                    return _instance;
                }
            }
        }
    }
}
