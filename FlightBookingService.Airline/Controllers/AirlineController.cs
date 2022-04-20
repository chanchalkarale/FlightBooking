using FlightBookingService.Airline.DTO.Request;
using FlightBookingService.Airline.DTO.Response;
using FlightBookingService.Airline.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightBookingService.Airline.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        #region
        private readonly IAirlineFlightDetailsServices _airlineFlightDetailsServices;
        #endregion

        #region Controller

        public AirlineController(IAirlineFlightDetailsServices airlineFlightDetailsServices)
        {
            _airlineFlightDetailsServices = airlineFlightDetailsServices ?? throw new ArgumentNullException(nameof(airlineFlightDetailsServices));
        }
         
        #endregion

   

        [HttpPost, ActionName("Inventory")]
        [HttpPost]
        public async Task<bool> Register([FromBody] AirlineFlightDetailsRequest airlineFlightDetailsRequest)
        {
            var result = await _airlineFlightDetailsServices.AddAirlineSchedule(airlineFlightDetailsRequest);

            return result;
        }

        // GET api/<AirlineController>/5
        [HttpGet("{searchText}"),ActionName("Search")]
        public async Task<AirlineFlightDetailsResponseList> Search(string searchText)
        {
            return await _airlineFlightDetailsServices.SearchFlight(searchText);
        }


        [HttpPost, ActionName("Booking")]
        [HttpPost]
        public async Task<bool> TicketBooking([FromBody] FlightBookingDetailsRequest flightBookingDetailsRequest)
        {
            var result = false;
            result = await _airlineFlightDetailsServices.BookFlightTicket(flightBookingDetailsRequest);
            return result;
        }


        // GET api/<AirlineController>/5
        [HttpGet("{pnr}")]
        public async Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr)
        {
            return await _airlineFlightDetailsServices.GetBookedTicketDetails(pnr);
        }
         

        // DELETE api/<AirlineController>/5
        [HttpDelete("{pnr}")]
        public async Task<bool> CancelTicket(string pnr)
        {
            var result = false;

            return result;
        }
    }
}
