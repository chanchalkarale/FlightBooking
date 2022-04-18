using FlightBookingService.User.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.User.Repository.Interface
{
    public interface IUserRegistrationServices
    {
        /// <summary>
        /// Add User Registrations
        /// </summary>
        /// <param name="userRegistrationRequest"></param>
        /// <returns></returns>
        Task<bool> RegisterUser(UserRegistrationRequest userRegistrationRequest);

        Task<bool> Login(LoginRequest loginRequest);
    }
}
