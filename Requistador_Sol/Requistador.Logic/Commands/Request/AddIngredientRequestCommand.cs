using AutoMapper;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Logic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Commands.Request
{
    public class AddIngredientRequestCommand : BaseCommand<IngredientDto>
    {
        private readonly IngredientDto _dto;
        public AddIngredientRequestCommand(IngredientDto dto)
        {
            _dto = dto;
        }

        public class Handler : BaseHandler<AddIngredientRequestCommand, IngredientDto>
        {
            public Handler(RequestDbContext requestDbContex, IMapper mapper) : base(requestDbContex, mapper) {}
            
            public override async Task<IngredientDto> Handle(AddIngredientRequestCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Ingredient>(request._dto);
                entity.Id = Guid.NewGuid().ToString();

                var appRequest = new AppRequest<BaseEntity>
                {
                    Id = Guid.NewGuid(),
                    RequestType = eAppRequestType.Add,
                    RequestStatus = eAppRequestStatus.Pending,
                    CreatedOn = DateTime.Now,
                    Entity = entity
                };

                await Task.FromResult(_requestDbContext.Insert(appRequest));

                return _mapper.Map<IngredientDto>(entity);
            }
        }
    }
}
