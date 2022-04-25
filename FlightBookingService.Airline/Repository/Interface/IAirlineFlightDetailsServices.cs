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
        Task<bool> AddAirlineDetails(AirlineDetailsRequest airlineDetailsRequest);
        Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest);
        

        Task<AirlineFlightDetailsResponseList> SearchFlight(string search);

        /// <summary>
        /// Using this function to add Flight booking ticket in table
        /// </summary>
        /// <param name="flightBookingDetailsRequest"></param>
        /// <returns></returns>
        Task<bool> BookFlightTicket(FlightBookingDetailsRequest flightBookingDetailsRequest);

        Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr);

        Task<bool> CancleBookTicket(string pnr);

        Task<BookedTicketDetailsResponseList> GetBookedTicketHistory(string emailId);

        /// <summary>
        /// Update airline : Blocked airline status=1 and unblock=0
        /// </summary>
        /// <param name="airlineId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateAirline(int airlineId,int status);

        Task<bool> AddDiscount(DiscountsRequest discountsRequest);

        Task<DiscountsResponseList> GetAllDiscounts();
    }
}
