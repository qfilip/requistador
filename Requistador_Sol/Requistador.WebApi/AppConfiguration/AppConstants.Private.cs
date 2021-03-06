﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public static partial class AppConstants
    {
        // common
        // used mostly for wwwroot folder/file paths //
        public const string AppUrl = "http://localhost:5655";
        public const string AppDbSourcePrefix = "Datasource=";
        public const string AppDbName = "requistador.db3";
        public const string AppIdentityDbName = "app_identity.db3";
        public const string AppRequestDbName = "request_db.db";
        public const string AppLogFolder = "syslogs";

        // identity
        // required by Requistador.Identity assembly
        public const string Id_AppId = "9600d2a2-0a75-4b08-9ce7-701aa0be175b";
        public const string Id_RsaKeysFile = "rsa_keys.json";

        // authentication
        public const string Auth_SecretKey = "hey hey Judy Judy";
        public const string Auth_ValidIssuer = "http://localhost:5655";
        public const string Auth_ValidAudience = "http://localhost:4200";
        public const string Auth_EncryptionAlgorithm = SecurityAlgorithms.HmacSha256;

        public const string Auth_AllowedOrigin = "http://localhost:4200";

        // namespaces
        public const string NMSP_Solution = "Requistador";
        public const string NMSP_Logic = "Requistador.Logic";
        public const string NMSP_DomainDtos = "Requistador.Dtos.Domain";
        public const string NMSP_WebApiDtos = "Requistador.Dtos.WebApi";
        public const string NMSP_IdentityDtos = "Requistador.Dtos.Identity";

        // formats
        public const string Format_SyslogTime = "yyyy_MM_dd_HH_mm_ss";

        // client
        public const string Client_File_ControllerMethods = "api.endpoints.ts";
        public const string Client_File_Constants = "constants.ts";
    }
}
