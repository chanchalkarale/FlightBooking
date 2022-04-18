using FlightBookingService.User.DTO.Request;
using FlightBookingService.User.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightBookingService.User.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistrationServices _userRegistrationServices;

        public RegistrationController(IUserRegistrationServices userRegistrationServices)
        {
            _userRegistrationServices = userRegistrationServices ?? throw new ArgumentNullException(nameof(userRegistrationServices));
        }


        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost, ActionName("Login")]
        public async Task<bool> Login([FromBody] LoginRequest loginRequest)
        {
            bool result = false;
            if(loginRequest!=null)
            {
                if(loginRequest.Username=="Admin@123" && loginRequest.Password=="Admin@123")
                {

                }
                else
                {
                  result= await _userRegistrationServices.Login(loginRequest);
                }

            }
            return result;
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
