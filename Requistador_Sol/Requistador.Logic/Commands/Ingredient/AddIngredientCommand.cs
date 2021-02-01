using Requistador.DataAccess.Contexts;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Logic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Commands
{
    public class AddIngredientCommand : BaseCommand<eAppRequestStatus>
    {
        private readonly Ingredient _entity;
        public AddIngredientCommand(Ingredient entity)
        {
            _entity = entity;
        }

        public class Handler : BaseHandler<AddIngredientCommand, eAppRequestStatus>
        {
            public Handler(AppDbContext dbContext) : base(dbContext) { }

            public override async Task<eAppRequestStatus> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
            {
                await _dbContext.Ingredients.AddAsync(request._entity);
                return eAppRequestStatus.Resolved;
            }
        }
    }
}
