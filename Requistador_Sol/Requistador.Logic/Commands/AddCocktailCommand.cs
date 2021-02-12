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
    public class AddCocktailCommand : BaseCommand<eAppRequestStatus>
    {
        private readonly Cocktail _entity;
        public AddCocktailCommand(Cocktail entity)
        {
            _entity = entity;
        }

        public class Handler : BaseHandler<AddCocktailCommand, eAppRequestStatus>
        {
            public Handler(AppDbContext dbContext) : base (dbContext) {}

            public override async Task<eAppRequestStatus> Handle(AddCocktailCommand request, CancellationToken cancellationToken)
            {
                await _dbContext.Cocktails.AddAsync(request._entity);
                return eAppRequestStatus.Resolved;
            }
        }
    }
}
