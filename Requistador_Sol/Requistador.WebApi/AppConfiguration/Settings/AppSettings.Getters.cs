using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Requistador.WebApi.AppConfiguration.Settings
{
    public sealed partial class AppSettings
    {
        public static SymmetricSecurityKey GetAppKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.Auth_SecretKey));

        // paths
        public static string GetAppDbConnection() => Instance._parameters.AppDbConnString;
        public static string GetIdentityDbConnection() => Instance._parameters.IdentityDbConnString;
        public static string GetRequestDbConnection() => Instance._parameters.RequestDbConnString;
        public static string GetSyslogPath() => Instance._parameters.SyslogPath;

        // app config
        public static int GetProcessingInterval() => Instance._parameters.RequestProcessingInterval;
    }
}
