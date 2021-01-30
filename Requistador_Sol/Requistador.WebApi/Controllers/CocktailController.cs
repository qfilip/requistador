﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Requistador.DataAccess.Contexts;
using Requistador.Logic.Queries.Cocktail;
using Requistador.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class CocktailController : BaseApiController
    {
        public CocktailController(IMediator mediator, AppDbContext dbContext, SyslogService syslogService)
            : base(mediator, dbContext, syslogService) {}


        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }
    }
}
