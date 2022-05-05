using FlightBookingService.User.DataContext;
using FlightBookingService.User.DTO.Request;
using FlightBookingService.User.Models;
using FlightBookingService.User.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.User.Repository.Services
{
    public class UserRegistrationServices : IUserRegistrationServices
    {
        #region

        private readonly UserServiceContext _userServiceContext;
        private readonly ILogger<UserRegistrationServices> _logger;


        #endregion

        #region Constructor

        public UserRegistrationServices(UserServiceContext userServiceContext, ILogger<UserRegistrationServices> logger)
        {
            _logger = logger;
            _userServiceContext = userServiceContext ?? throw new ArgumentNullException(nameof(userServiceContext));
        }
        #endregion
        public async Task<bool> RegisterUser(UserRegistrationRequest userRegistrationRequest)
        {
            if (userRegistrationRequest == null)
                throw new ArgumentNullException(nameof(userRegistrationRequest));

            bool result = false;
            UserRegistration userRegistration;

            var checkUserExists = await _userServiceContext.userRegistrations.Where(d => d.UserName == userRegistrationRequest.UserName).FirstOrDefaultAsync();
            if(checkUserExists==null)
            {
                userRegistration = new UserRegistration()
                {
                    UserName = userRegistrationRequest.UserName,
                    Password=userRegistrationRequest.PassWord,
                    Name = userRegistrationRequest.Name,
                    EmailId=userRegistrationRequest.EmailId,
                    InsertDate=DateTime.Now,
                    Flag=0, 
                };

                await _userServiceContext.AddAsync(userRegistration);
                await _userServiceContext.SaveChangesAsync();
                result = true;
            }


            return  result;
        }

        public bool Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                throw new ArgumentNullException(nameof(loginRequest));

            bool result = false;
            var checkUser =  _userServiceContext.userRegistrations.Where(d => d.UserName == loginRequest.Username
                                                 && d.Password == loginRequest.Password).FirstOrDefault();

            if(checkUser!=null)
            {
                result = true;
            }

            return result;
        }
    }
}
