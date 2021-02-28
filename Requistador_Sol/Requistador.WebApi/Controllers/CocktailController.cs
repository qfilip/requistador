using Microsoft.AspNetCore.Mvc;
using Requistador.Logic.Commands;
using Requistador.Logic.Queries.Cocktail;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class CocktailController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await Mediator.Send(new CreateCocktailCommand(null));
            return Ok(result);
        }
    }
}
