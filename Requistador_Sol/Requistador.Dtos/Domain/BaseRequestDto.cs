using Newtonsoft.Json;
using Requistador.Domain.Enumerations;
using System;

namespace Requistador.Dtos.Domain
{
    public class BaseRequestDto<T> where T : BaseDto
    {
        public Guid Id { get; set; }
        public eEntityStatus EntityStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public eAppRequestType RequestType { get; set; }
        public eAppRequestStatus RequestStatus { get; set; }

        public Guid EntityId { get; set; }
        public eEntityTable EntityTable { get; set; }

        public string DetailsJson
        { 
            get => JsonConvert.SerializeObject(DetailsObject);
            set
            {
                if (value != null)
                    DetailsObject = JsonConvert.DeserializeObject<T>(value);
            }
        }
        public T DetailsObject { get; set; }
    }
}
