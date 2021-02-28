using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Dtos.Domain
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
