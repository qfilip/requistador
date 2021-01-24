using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class CocktailController : BaseApiController
    {
        public async Task<IActionResult> GetAll()
        {
            var result = await Task.FromResult("test");
            return Ok(result);
        }
    }
}
