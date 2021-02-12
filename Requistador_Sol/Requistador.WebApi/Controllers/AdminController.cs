using Microsoft.AspNetCore.Mvc;
using Requistador.Dtos.WebApi;
using Requistador.WebApi.Services;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly AppStateService _appConfigService;
        public AdminController(AppStateService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppConfiguration()
        {
            var result = await Task.FromResult(_appConfigService.GetAppSettings());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetRequestProcessingInterval([FromBody] AppStateDto dto)
        {
            var result = await Task.FromResult(_appConfigService.SetProcessingInterval(dto));
            return Ok(result);
        }
    }
}
