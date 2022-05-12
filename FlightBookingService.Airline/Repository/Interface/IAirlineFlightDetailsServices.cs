using FlightBookingService.Airline.DTO.Request;
using FlightBookingService.Airline.DTO.Response;
using FlightBookingService.Airline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Repository.Interface
{
    public interface IAirlineFlightDetailsServices
    {

        /// <summary>
        /// Using this api add airline deatils
        /// </summary>
        /// <param name="airlineDetailsRequest"></param>
        /// <returns></returns>
        Task<bool> AddAirlineDetails(AirlineDetailsRequest airlineDetailsRequest);


        /// <summary>
        /// Add Airline Inventory Schedule
        /// </summary>
        /// <param name="airlineFlightDetailsRequest"></param>
        /// <returns></returns>
        Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest);
        

        /// <summary>
        /// Search flight based on any string.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<AirlineFlightDetailsResponseList> SearchFlight(string search);

        /// <summary>
        /// Using this function to add Flight booking ticket in table
        /// </summary>
        /// <param name="flightBookingDetailsRequest"></param>
        /// <returns></returns>
        Task<bool> BookFlightTicket(FlightBookingDetailsRequest flightBookingDetailsRequest);


        /// <summary>
        /// Get Booked Ticket Details
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr);


        /// <summary>
        /// Cancle Booked Ticket
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        Task<bool> CancleBookTicket(string pnr);


        /// <summary>
        /// Get booked ticket detailed history
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task<BookedTicketDetailsResponseList> GetBookedTicketHistory(string emailId);

        /// <summary>
        /// Update airline : Blocked airline status=1 and unblock=0
        /// </summary>
        /// <param name="airlineId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateAirline(int airlineId,int status);

        /// <summary>
        /// Add discount information
        /// </summary>
        /// <param name="discountsRequest"></param>
        /// <returns></returns>
        Task<bool> AddDiscount(DiscountsRequest discountsRequest);


        /// <summary>
        /// Get all discounts information
        /// </summary>
        /// <returns></returns>
        Task<DiscountsResponseList> GetAllDiscounts();

        /// <summary>
        /// Get unblock airlines for ticket booking page.
        /// </summary>
        /// <returns></returns>
        Task<List<GetAirlineResponse>> GetAirlines();

        /// <summary>
        /// Get all(blocked or unblocked) airlines
        /// </summary>
        /// <returns></returns>
        Task<List<GetAirlineResponse>> GetAllAirlines();

        /// <summary>
        /// Get all airline flights details(Inventory details)
        /// </summary>
        /// <returns></returns>
        Task<AirlineFlightDetailsResponseList> GetAllAirlineFlightsDetails();


        /// <summary>
        /// Remove Airlie flight from list
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Task<bool> RemoveAirlineFlight(int flightId);


        /// <summary>
        /// Delete airline from list
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns></returns>
        Task<bool> DeleteAirline(int airlineId);

        /// <summary>
        /// Remove Discount from list
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        Task<bool> RemoveDiscount(int discountId);

        /// <summary>
        /// Update discount 
        /// </summary>
        /// <param name="discountsRequest"></param>
        /// <returns></returns>
        Task<bool> UpdateDiscount(DiscountsRequest discountsRequest);

        /// <summary>
        /// Search Flights for booking (Here I used RAW Query)
        /// </summary>
        /// <param name="searchFlightRequest"></param>
        /// <returns></returns>
        Task<List<AirlineFlightDetailsRawQueryModel>> SearchFlights(SearchFlightRequest searchFlightRequest);

        /// <summary>
        /// Get Discount details using discountCode
        /// </summary>
        /// <param name="discountCode"></param>
        /// <returns></returns>
        Task<DiscountsResponseList> GetDiscountUsingCode(string discountCode);


        /// <summary>
        /// Get all booked ticket details using userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<BookedTicketDetailsResponseList> GetAllBookedTicket(int userId);


        /// <summary>
        /// Get booked ticket history using user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<BookedTicketDetailsResponseList> GetBookedTicketHistoryViaUserId(int userId);
    }
}
