using Microsoft.AspNetCore.Mvc;
using Requistador.Domain.Dtos;
using Requistador.Logic.Commands.Request;
using Requistador.Logic.Queries.Ingredient;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class IngredientController : BaseApiController
    {
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }

        public async Task<IActionResult> Create([FromBody] IngredientDto dto)
        {
            var result = await Mediator.Send(new AddIngredientRequestCommand(dto));
            return Ok(result);
        }
    }
}
