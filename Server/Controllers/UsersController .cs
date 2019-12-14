using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaneServer.Server.DTO;
using SaneServer.Server.Services;


namespace SaneServer.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] UserDTO userCreds)
        {
            if (ModelState.IsValid)
            {
                var userAuthResp = _userService.Authenticate(userCreds.UserName, userCreds.Password);

                if (userAuthResp == null)
                {
                    return Unauthorized();
                }

                return Ok(userAuthResp);
            }
            return BadRequest(ModelState);
        }
    }
}