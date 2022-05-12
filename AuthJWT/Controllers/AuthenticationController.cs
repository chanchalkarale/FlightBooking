using AuthJWT.Model;
using AuthJWT.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthenticationController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "Value1", "Value2" };
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var token = _authManager.Authenticate(userCredentials.UserName, userCredentials.Password);
            if (token is null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
