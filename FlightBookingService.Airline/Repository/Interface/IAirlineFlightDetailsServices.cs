using FlightBookingService.Airline.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Repository.Interface
{
    public interface IAirlineFlightDetailsServices
    {
        Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest);
    }
}
