using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.FluentConfigurations
{
    public class AngularMethodAttribute : TsFunctionAttribute
    {
        public AngularMethodAttribute(Type returnType)
        {
            // Here we override method return type for TypeScript export
            StrongType = returnType;
        }

        public bool IsArrayBuffer { get; set; }

    }
}
