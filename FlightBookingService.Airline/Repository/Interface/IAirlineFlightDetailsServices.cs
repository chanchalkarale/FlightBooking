using FlightBookingService.Airline.DTO.Request;
using FlightBookingService.Airline.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Repository.Interface
{
    public interface IAirlineFlightDetailsServices
    {
        Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest);
        

        Task<AirlineFlightDetailsResponseList> SearchFlight(string search);

        /// <summary>
        /// Using this function to add Flight booking ticket in table
        /// </summary>
        /// <param name="flightBookingDetailsRequest"></param>
        /// <returns></returns>
        Task<bool> BookFlightTicket(FlightBookingDetailsRequest flightBookingDetailsRequest);

        Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr);
    }
}
