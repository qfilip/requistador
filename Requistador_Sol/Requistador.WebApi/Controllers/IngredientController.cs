using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requistador.DataAccess.Contexts;
using Requistador.Logic.Queries.Ingredient;
using Requistador.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class IngredientController : BaseApiController
    {
        public IngredientController(IMediator mediator, AppDbContext dbContext, SyslogService syslogService)
            : base(mediator, dbContext, syslogService) { }


        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }
    }
}
