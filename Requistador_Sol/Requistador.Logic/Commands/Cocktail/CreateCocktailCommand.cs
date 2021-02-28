using FluentValidation;
using MapsterMapper;
using MediatR;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Dtos.Domain;
using Requistador.Logic.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Commands
{
    public class CreateCocktailCommand : BaseCommand<AppRequestDto<CocktailDto>>
    {
        private readonly CocktailDto Dto;
        public CreateCocktailCommand(CocktailDto dto)
        {
            Dto = dto;
        }

        public class Validator : AbstractValidator<CreateCocktailCommand>
        {
            public Validator()
            {
                RuleFor(x => x).NotNull().WithMessage("Request cannot be null");
                RuleFor(x => x.Dto).NotNull().WithMessage("Request dto cannot be null");
            }
        }

        public class Handler : BaseHandler<CreateCocktailCommand, AppRequestDto<CocktailDto>>
        {
            public Handler(AppDbContext dbContext, IMapper mapper, IMediator mediator) : base (dbContext, mapper, mediator) {}

            public override async Task<AppRequestDto<CocktailDto>> Handle(CreateCocktailCommand command, CancellationToken cancellationToken)
            {
                command.Dto.Id = Guid.NewGuid();
                await GeneratePendingRequest(command.Dto, eAppRequestType.Add, eEntityTable.Cocktail);
                
                var cocktail = _mapper.Map<Cocktail>(command.Dto);
                await _dbContext.Cocktails.AddAsync(cocktail);
                await _dbContext.SaveChangesAsync();

                return await GenerateResolvedRequest<CocktailDto>();
            }
        }
    }
}
