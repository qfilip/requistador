using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using Requistador.Logic.Commands.Request;
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

        public async Task<IActionResult> Create([FromBody] IngredientDto dto)
        {
            var result = await _mediator.Send(new AddIngredientRequestCommand(dto));
            return Ok(result);
        }
    }
}
