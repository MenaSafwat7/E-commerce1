using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servises.Abstractions;
using Shared;
using Shared.ErrorModels;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthunticationController(IServiceManager serviceManager): ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO login) => Ok(serviceManager.autionticationService.LoginAsync(login));


        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(UsserRegesterDTO Register) => Ok(serviceManager.autionticationService.RegesterAsync(Register));


        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return Ok(serviceManager.autionticationService.CheckEmailExist(email));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.autionticationService.GetUserAddress(email);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserResultDTO>> UpdateAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.autionticationService.UpdateUserAddress(email,address);
            return Ok(result);
        }
    }
}
