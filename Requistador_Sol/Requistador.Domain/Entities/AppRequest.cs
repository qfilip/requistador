using Requistador.Domain.Base;
using System;

namespace Requistador.Domain.Entities
{
    public class AppRequest : BaseRequestEntity
    {
        public Guid PendingRequestId { get; set; }
        public AppRequest PendingRequest { get; set; }
    }
}
