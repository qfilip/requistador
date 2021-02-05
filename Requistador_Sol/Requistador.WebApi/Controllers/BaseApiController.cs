using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Requistador.DataAccess.Contexts;
using Requistador.WebApi.Services;

namespace Requistador.WebApi.Controllers
{
    [ApiController]
    [EnableCors]
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private AppDbContext _dbContext;
        private SyslogService _syslogService;

        public IMediator Mediator =>
            _mediator ?? 
            (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        public AppDbContext AppDbContext =>
            _dbContext ??
            (AppDbContext)HttpContext.RequestServices.GetService(typeof(AppDbContext));

        public SyslogService SyslogService =>
            _syslogService ??
            (SyslogService)HttpContext.RequestServices.GetService(typeof(SyslogService));


        [HttpGet]
        public IActionResult TestLogging()
        {
            SyslogService.CreateTestLog();
            return Ok();
        }
    }
}
