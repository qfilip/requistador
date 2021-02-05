using Microsoft.AspNetCore.Mvc;
using Requistador.Logic.Queries.Cocktail;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class CocktailController : BaseApiController
    {
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }
    }
}
