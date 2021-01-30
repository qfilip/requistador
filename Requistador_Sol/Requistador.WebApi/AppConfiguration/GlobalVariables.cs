using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public static class GlobalVariables
    {
        public const string AppUrl = "http://localhost:5655";
        public const string AppDbSourcePrefix = "Datasource=";
        public const string AppDbName = "requistador.db3";
        public const string AppRequestDbName = "request_db.db";
        public const string AppLogFolder = "syslogs";

        // namespaces
        public const string NMSP_Solution = "Requistador";
        public const string NMSP_Logic = "Requistador.Logic";
        public const string NMSP_DomainDtos = "Requistador.Domain.Dtos";

        // formats
        public const string Format_SyslogTime = "yyyy_MM_dd_HH_mm_ss";
        // client
        public static string ClientFile_ControllerMethods = "api.endpoints.ts";
    }
}
