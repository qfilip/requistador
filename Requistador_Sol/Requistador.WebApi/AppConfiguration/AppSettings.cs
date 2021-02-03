using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public class AppSettings
    {
        public AppSettings(string appDbPath, string liteDbPath)
        {
            DbConnectionString = appDbPath;
            LiteDbPath = liteDbPath;
        }

        public string DbConnectionString { get; private set; }
        public string LiteDbPath { get; private set; }
        
        public static SymmetricSecurityKey GetAppKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.Auth_SecretKey));
    }
}
