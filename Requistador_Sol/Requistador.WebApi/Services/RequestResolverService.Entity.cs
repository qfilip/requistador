using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using Requistador.Logic.Commands;
using System;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public partial class RequestResolverService<T> where T : BaseEntity, new()
    {
        private async Task<eAppRequestStatus> ResolveCocktailRequestAsync(AppRequest<T> clientRequest)
        {
            throw new NotImplementedException();
        }

        private async Task<eAppRequestStatus> ResolveIngredientRequestAsync(AppRequest<T> clientRequest)
        {
            var entity = clientRequest.Entity as Ingredient;
            var result = eAppRequestStatus.None;

            if(clientRequest.RequestType == eAppRequestType.Add)
            {
                result = await _mediator.Send(new AddIngredientCommand(entity));
            }

            return result;
        }

        private async Task<eAppRequestStatus> ResolveExcerptRequestAsync(AppRequest<T> clientRequest)
        {
            throw new NotImplementedException();
        }
    }
}
