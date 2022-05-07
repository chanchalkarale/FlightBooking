using FlightBookingService.User.DTO.Request;
using FlightBookingService.User.Models;
using FlightBookingService.User.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightBookingService.User.Controllers
{
    public class LoginResponse
    {
      public string role = "";
      public  string jwtToken = "";
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistrationServices _userRegistrationServices;
        private readonly ILogger<RegistrationController> _logger;
        private readonly IAuthManager _authManager;

        public RegistrationController(IUserRegistrationServices userRegistrationServices, ILogger<RegistrationController> logger, IAuthManager authManager)
        {
             _logger = logger;
            _userRegistrationServices = userRegistrationServices ?? throw new ArgumentNullException(nameof(userRegistrationServices));
            _authManager = authManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var loginResponse = new LoginResponse();
            var token = _authManager.Authenticate(userCredentials.UserName, userCredentials.Password);
            if (token is null)
            {
                //return Unauthorized();
            }

            loginResponse.jwtToken = token;
            loginResponse.role = "Admin";
            return Ok(token);
        }

        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost, ActionName("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            int userId = 0;
            var loginResponse = new LoginResponse();
            string role = "user";
            var token = "";
            if (loginRequest!=null)
            {
                if(loginRequest.Username=="Admin" && loginRequest.Password=="Admin")
                {
                    role = "admin"; 
                }
                else
                {
                    role = "user"; 
                }

                userId = _userRegistrationServices.Login(loginRequest);

                if (userId > 0)
                {

                     token = _authManager.Authenticate(loginRequest.Username, loginRequest.Password, role,userId);
                    if (token is null)
                    {
                        return Unauthorized();
                    }
                    return Ok(token);
                }
            }
            return Unauthorized();
            
        }

        // POST api/<RegistrationController>
        [HttpPost, ActionName("register")]
        [HttpPost]
        public async Task<bool> Register([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            var result = await _userRegistrationServices.RegisterUser(userRegistrationRequest);

            return result;
        }

        // PUT api/<RegistrationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegistrationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
