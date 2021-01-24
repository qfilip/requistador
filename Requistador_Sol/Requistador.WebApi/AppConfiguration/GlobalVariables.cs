using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public static class GlobalVariables
    {
        public static string AppUrl = "http://localhost:5655";
        public static string AppDbSourcePrefix = "Datasource=";
        public static string AppDbName = "db.db3";
        public static string AppRequestDbName = "request_db.db";

        // namespaces
        public static string NMSP_Solution = "Requistador";
        public static string NMSP_Logic = "Requistador.Logic";
        public static string NMSP_DomainDtos = "Requistador.Domain.Dtos";

        // client
        public static string ClientFile_ControllerMethods = "api.endpoints.ts";
    }
}
