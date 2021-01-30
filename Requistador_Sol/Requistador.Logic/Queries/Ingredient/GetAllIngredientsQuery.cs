using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Dtos;
using Requistador.Logic.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Queries.Ingredient
{
    public class GetAllIngredientsQuery : BaseQuery<List<IngredientDto>>
    {

        public class Handler : BaseHandler<GetAllIngredientsQuery, List<IngredientDto>>
        {
            public Handler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

            public override async Task<List<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
            {
                var entities = await _dbContext.Ingredients.ToListAsync();
                var result = _mapper.Map<List<IngredientDto>>(entities);

                return result;
            }
        }
    }
}
