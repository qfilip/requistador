using Requistador.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Dtos.Domain
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
