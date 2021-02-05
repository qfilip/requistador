using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Requistador.WebApi.AppConfiguration;
using Requistador.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IActionResult> Login([FromBody] string password)
        {
            var token = _authService.Login();
            return Ok(new { Token = token });
        }
    }
}
