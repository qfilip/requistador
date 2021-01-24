using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
