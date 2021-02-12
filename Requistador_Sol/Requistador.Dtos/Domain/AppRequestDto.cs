using LiteDB;
using Requistador.Domain.Base;
using Requistador.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Dtos.Domain
{
    public class AppRequestDto<TDto> where TDto : BaseDto
    {
        [BsonId(false)]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public eAppRequestType RequestType { get; set; }
        public eAppRequestStatus RequestStatus { get; set; }
        public TDto Entity { get; set; }

        public Guid PendingRequestId { get; set; }
        public AppRequestDto<TDto> PendingRequest { get; set; }
    }
}
