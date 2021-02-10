using Requistador.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly AppConfigService _appConfigService;
        public AdminController(AppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }
    }
}
