using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Requistador.Dtos.Identity;
using Requistador.WebApi.FluentConfigurations;
using Requistador.WebApi.Services;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AuthService _authService;
        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [AngularMethod(typeof(bool))]
        public async Task<IActionResult> Login([FromBody] AppUserDto user)
        {
            var result = await _authService.Login();
            return Ok(result);
        }
    }
}
