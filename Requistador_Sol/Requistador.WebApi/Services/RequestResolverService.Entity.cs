using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using Requistador.Domain.Enumerations;
using System.Threading.Tasks;

namespace Requistador.WebApi.Services
{
    public partial class RequestResolverService<T> where T : BaseEntity
    {
        private async Task<eAppRequestStatus> ResolveCocktailRequestAsync(AppRequest<T> clientRequest)
        {

        }

        private async Task<eAppRequestStatus> ResolveIngredientRequestAsync(AppRequest<T> clientRequest)
        {

        }

        private async Task<eAppRequestStatus> ResolveExcerptRequestAsync(AppRequest<T> clientRequest)
        {

        }
    }
}
