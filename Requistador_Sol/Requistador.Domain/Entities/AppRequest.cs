using LiteDB;
using Requistador.Domain.Base;
using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Domain.Entities
{
    public class AppRequest<TEntity> where TEntity : BaseEntity
    {
        [BsonId(false)]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public eAppRequestType RequestType { get; set; }
        public eAppRequestStatus RequestStatus { get; set; }
        public TEntity Entity { get; set; }

        public Guid PendingRequestId { get; set; }
        public AppRequest<TEntity> PendingRequest { get; set; }
    }
}
