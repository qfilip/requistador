using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Requistador.DataAccess.Contexts;
using Requistador.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    [ApiController]
    [EnableCors]
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly AppDbContext _dbContext;
        protected readonly SyslogService _syslogService;

        public BaseApiController(IMediator mediator, AppDbContext dbContext, SyslogService syslogService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _syslogService = syslogService;
        }

        [HttpGet]
        public IActionResult TestLogging()
        {
            _syslogService.CreateTestLog();
            return Ok();
        }
    }
}
