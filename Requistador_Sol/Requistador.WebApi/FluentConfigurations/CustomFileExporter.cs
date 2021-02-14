using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Requistador.WebApi.FluentConfigurations
{
    public static class CustomFileExporter
    {
        public static string ExportDir;

        public static void ExportApiEndpoints()
        {
            var endpoints = new List<string>();
            var controllers = FindSubClassesOf<BaseApiController>();

            var methodsToExclude = typeof(BaseApiController)
                .GetMethods()
                .Select(x => x.Name)
                .ToList();

            // add api itself
            endpoints.Add("// api root");
            endpoints.Add($"export const Api_Root = '{AppConstants.AppUrl}';");
            endpoints.Add(string.Empty);

            foreach (var controller in controllers)
            {
                var methodNames = controller.GetMethods()
                    .Select(x => x.Name)
                    .Where(x => !methodsToExclude.Contains(x));

                endpoints.Add(CreateComment(controller.Name));

                foreach (var methodName in methodNames)
                    endpoints.Add(MakeEndpoint(controller.Name, methodName));

                // add new line for readabilty
                endpoints.Add(string.Empty);
            }

            WriteToFile(AppConstants.Client_File_ControllerMethods, endpoints);
        }

        public static void ExportPublicConstants()
        {
            var constants = new List<KeyValuePair<string, string>>()
            {
                // Ls - localStorage
                KeyValuePair.Create(nameof(AppConstants.PubConst_JwtKey), AppConstants.PubConst_JwtKey),
                KeyValuePair.Create(nameof(AppConstants.PubConst_ProcessingTimeoutMin), AppConstants.PubConst_ProcessingTimeoutMin.ToString())
            };

            var lines = new List<string>();
            foreach(var c in constants)
            {
                var line = $"export const {c.Key} = {c.Value};";
                lines.Add(line);
            }

            WriteToFile(AppConstants.Client_File_Constants, lines);
        }

        private static IEnumerable<Type> FindSubClassesOf<TBaseType>() where TBaseType : class
        {
            var baseType = typeof(TBaseType);
            var assembly = baseType.Assembly;

            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
        }

        private static string CreateComment(string controller)
        {
            controller = controller.Replace("Controller", string.Empty);
            return $"// {controller}s";
        }

        private static string MakeEndpoint(string controller, string method)
        {
            controller = controller.Replace("Controller", string.Empty);
            return $"export const {controller}s_{method} = '{AppConstants.AppUrl}/{controller}/{method}';";
        }

        private static void WriteToFile(string filename, List<string> lines)
        {
            var path = Path.Combine(ExportDir, filename);
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllLines(path, lines);
        }
    }
}
