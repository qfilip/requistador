using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Domain.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
