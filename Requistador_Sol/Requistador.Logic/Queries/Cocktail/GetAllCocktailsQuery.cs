using FluentValidation;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Dtos;
using Requistador.Logic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Queries.Cocktail
{
    public class GetAllCocktailsQuery : BaseQuery<List<CocktailDto>>
    {

        public class Handler : BaseHandler<GetAllCocktailsQuery, List<CocktailDto>>
        {
            public Handler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) {}

            public override async Task<List<CocktailDto>> Handle(GetAllCocktailsQuery request, CancellationToken cancellationToken)
            {
                var entities = await _dbContext.Cocktails.ToListAsync();
                var result = _mapper.Map<List<CocktailDto>>(entities);

                return result;
            }
        }
    }
}
