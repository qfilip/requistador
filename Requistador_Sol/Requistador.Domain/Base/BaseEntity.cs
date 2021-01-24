using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Domain.Base
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
