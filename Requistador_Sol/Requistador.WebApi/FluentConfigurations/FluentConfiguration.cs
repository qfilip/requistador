﻿using Reinforced.Typings.Fluent;
using Requistador.Domain.Base;
using Requistador.Domain.Enumerations;
using Requistador.Identity.Dtos;
using Requistador.Identity.Enumerations;
using Requistador.WebApi.AppConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Requistador.WebApi.FluentConfigurations
{
    public static class FluentConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            CustomFileExporter.ExportDir = builder.Context.TargetDirectory;
            CustomFileExporter.ExportApiEndpoints();

            var dtos = new List<Type>();

            var domainDtos = GetTypesForExport<BaseDto>(AppConstants.NMSP_DomainDtos);
            var identityDtos = GetTypesForExport<AppUserDto>(AppConstants.NMSP_IdentityDtos);

            dtos.AddRange(domainDtos);
            dtos.AddRange(identityDtos);

            builder.Global(cfg => cfg.CamelCaseForProperties().UseModules());

            builder.ExportAsInterfaces(dtos, cfg =>
                cfg.WithPublicProperties()
                .ExportTo("interfaces.ts"));

            builder.ExportAsEnums(new Type[] {
                // domain
                typeof(eAppRequestStatus),
                typeof(eAppRequestType),
                typeof(eEntityStatus),

                // identity
                typeof(eUserStatus),
                typeof(eUserRole)
            },
            cfg => cfg.ExportTo("enums.ts"));
        }

        private static Type[] GetTypesForExport<T>(string namespaceFilter)
        {
            return Assembly
                .GetAssembly(typeof(T))
                .ExportedTypes
                .Where(i => i.Namespace.StartsWith(namespaceFilter))
                .OrderBy(i => i.Name)
                .OrderBy(i => i.Name != nameof(T))
                .ToArray();
        }
    }
}
