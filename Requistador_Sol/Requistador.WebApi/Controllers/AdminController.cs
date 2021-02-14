using Microsoft.AspNetCore.Mvc;
using Requistador.Dtos.WebApi;
using Requistador.WebApi.Services;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService appConfigService)
        {
            _adminService = appConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppConfiguration()
        {
            var result = await Task.FromResult(_adminService.GetAppSettings());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLogFile(string filename)
        {
            var result = await _adminService.GetLogFileAsync(filename);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> HandleAdminRequest([FromBody] ApiAdminRequestDto dto)
        {
            // var result = _adminService
            return Ok();
        }
    }
}
