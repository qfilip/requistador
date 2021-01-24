using LiteDB;
using Requistador.Domain.Base;
using Requistador.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Entities
{
    public class ClientRequest<TEntity> where TEntity : BaseEntity
    {
        [BsonId(false)]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public eClientRequestType RequestType { get; set; }
        public eClientRequestStatus RequestStatus { get; set; }
        public TEntity Entity { get; set; }

        public Guid SourceRequestId { get; set; }
        public ClientRequest<TEntity> SourceRequest { get; set; }
    }
}
