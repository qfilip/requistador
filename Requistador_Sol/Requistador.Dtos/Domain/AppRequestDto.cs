using System;

namespace Requistador.Dtos.Domain
{
    public class AppRequestDto<T> : BaseRequestDto<T> where T : BaseDto
    {
        public Guid PendingRequestId { get; set; }
        public AppRequestDto<T> PendingRequest { get; set; }
    }
}
