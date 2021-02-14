using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Dtos.WebApi
{
    public class ApiAdminRequest
    {
        public eAdminRequestFor RequestFor { get; set; }
        public List<string> Args { get; set; }
    }
}
