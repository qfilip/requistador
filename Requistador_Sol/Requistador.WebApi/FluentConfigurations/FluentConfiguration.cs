using Reinforced.Typings.Fluent;
using Requistador.Domain.Enumerations;
using Requistador.WebApi.AppConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Requistador.WebApi.FluentConfigurations
{
    public static class FluentConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            CustomFileExporter.ExportDir = builder.Context.TargetDirectory;
            CustomFileExporter.ExportApiEndpoints();
            
            var dtos = Assembly.GetAssembly(typeof(Domain.Base.BaseDto)).ExportedTypes
                .Where(i => i.Namespace.StartsWith(AppConstants.NMSP_DomainDtos))
                .OrderBy(i => i.Name)
                .OrderBy(i => i.Name != nameof(Domain.Base.BaseDto))
                .ToArray();

            builder.Global(cfg => cfg.CamelCaseForProperties().UseModules());

            builder.ExportAsInterfaces(dtos, cfg =>
                cfg.WithPublicProperties()
                .ExportTo("interfaces.ts"));

            builder.ExportAsEnums(new Type[] {
                typeof(eAppRequestStatus),
                typeof(eAppRequestType),
                typeof(eEntityStatus)
            },
            cfg => cfg.ExportTo("enums.ts"));
        }
    }
}
