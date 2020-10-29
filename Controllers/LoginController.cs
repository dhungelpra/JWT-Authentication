using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT_Authentication.Models;
using JWT_Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticateService _jwtAuthenticateService;

        public LoginController(IJwtAuthenticateService jwtAuthenticateService)
        {
            _jwtAuthenticateService = jwtAuthenticateService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]User users)
        {
            var user = _jwtAuthenticateService.Authenticate(users.UserName, users.Password);
            if (user == null)
            {
                return BadRequest(new { message = "UserName or Password Invaid" });
            }
            return Ok(user);
        }
    }
}
