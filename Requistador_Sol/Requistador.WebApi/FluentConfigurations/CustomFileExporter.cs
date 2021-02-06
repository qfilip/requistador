﻿using Requistador.WebApi.AppConfiguration;
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

            var path = Path.Combine(ExportDir, AppConstants.Client_File_ControllerMethods);
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllLines(path, endpoints);
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
    }
}
