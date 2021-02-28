using LiteDB;
using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Domain.Base
{
    public class BaseRequestEntity
    {
        public BaseRequestEntity()
        {
            CreatedOn = DateTime.Now;
        }

        [BsonId(false)]
        public Guid Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }

        public eAppRequestType RequestType { get; set; }
        public eAppRequestStatus RequestStatus { get; set; }

        public string DetailsJson { get; set; }
        public Guid EntityId { get; set; }
        public eEntityTable EntityTable { get; set; }
    }
}
