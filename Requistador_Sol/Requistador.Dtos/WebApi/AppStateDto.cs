using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Dtos.WebApi
{
    public class AppStateDto : ApiBaseDto
    {
        public int ProcessingInterval { get; set; }
        public List<string> SyslogFiles { get; set; }
    }
}
