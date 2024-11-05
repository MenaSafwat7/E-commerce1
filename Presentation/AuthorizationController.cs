using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthorizationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
        {
            var result = await serviceManager.autionticationService.LoginAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("Regester")]
        public async Task<ActionResult<UserResultDTO>> Regester(UsserRegesterDTO regesterDTO)
        {
            var result = await serviceManager.autionticationService.RegesterAsync(regesterDTO);
            return Ok(result);
        }
    }
}
