using FlightBookingService.Airline.DTO.Request;
using FlightBookingService.Airline.DTO.Response;
using FlightBookingService.Airline.Models;
using FlightBookingService.Airline.Repository.Interface;
using FlightBookingService.User.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Repository.Services
{
    public class AirlineFlightDetailsServices : IAirlineFlightDetailsServices
    {

        #region 
        private readonly AirlineServiceContext _airlineServiceContext;
        #endregion

        #region Controller
        public AirlineFlightDetailsServices(AirlineServiceContext airlineServiceContext)
        {
            _airlineServiceContext = airlineServiceContext ?? throw new ArgumentNullException(nameof(airlineServiceContext));
        }
        #endregion

        public async Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest)
        {
            if (airlineFlightDetailsRequest == null)
                throw new ArgumentNullException(nameof(airlineFlightDetailsRequest));

            bool result = false;
            AirlineFlightDetails airlineFlightDetails;

            var airlineDetails = await _airlineServiceContext.AirlineFlightDetails.Where(d => d.FlightNumber == airlineFlightDetailsRequest.FlightNumber).FirstOrDefaultAsync();
            if(airlineDetails==null)
            {
                airlineFlightDetails = new AirlineFlightDetails() { 
                  FlightNumber=airlineFlightDetailsRequest.FlightNumber,
                  Airline=airlineFlightDetailsRequest.Airline,
                  FromPlaceName=airlineFlightDetailsRequest.FromPlaceName,
                  ToPlaceName=airlineFlightDetailsRequest.ToPlaceName,
                  FlightStartDateTime=airlineFlightDetailsRequest.FlightStartDateTime,
                  FlightToDateTime=airlineFlightDetailsRequest.FlightToDateTime,
                  TotalBusinessSeats=airlineFlightDetailsRequest.TotalBusinessSeats,
                  TotalNonBusinessSeats=airlineFlightDetailsRequest.TotalNonBusinessSeats,
                  TicketCost=airlineFlightDetailsRequest.TicketCost,
                  FlightSeatRow=airlineFlightDetailsRequest.FlightSeatRow,
                  Meal=airlineFlightDetailsRequest.Meal,
                  CreateDate=DateTime.Now,
                  Flag=0
                };
                await _airlineServiceContext.AddAsync(airlineFlightDetails);
            }
            else
            {
                airlineDetails.Airline = airlineFlightDetailsRequest.Airline;
                airlineDetails.FromPlaceName = airlineFlightDetailsRequest.FromPlaceName;
                airlineDetails.ToPlaceName = airlineFlightDetailsRequest.ToPlaceName;
                airlineDetails.FlightStartDateTime = airlineFlightDetailsRequest.FlightStartDateTime;
                airlineDetails.FlightToDateTime = airlineFlightDetailsRequest.FlightToDateTime;
                airlineDetails.TotalBusinessSeats = airlineFlightDetailsRequest.TotalBusinessSeats;
                airlineDetails.TotalNonBusinessSeats = airlineFlightDetailsRequest.TotalNonBusinessSeats;
                airlineDetails.TicketCost = airlineFlightDetailsRequest.TicketCost;
                airlineDetails.FlightSeatRow = airlineFlightDetailsRequest.FlightSeatRow;
                airlineDetails.Meal = airlineFlightDetailsRequest.Meal;
                airlineDetails.CreateDate = DateTime.Now;
                airlineDetails.Flag = 0; 
            }

            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }

        public async Task<AirlineFlightDetailsResponseList> SearchFlight(string search)
        {
             

        
            var searchList = await _airlineServiceContext.AirlineFlightDetails.Where(d => d.Airline.Contains(search) ||
                                                       d.FlightNumber.Contains(search) ||
                                                       d.ToPlaceName.Contains(search) ||
                                                       d.FromPlaceName.Contains(search)).Select(p=> new AirlineFlightDetailsResponse { 
                                                          FlightNumber=p.FlightNumber,
                                                          Airline=p.Airline,
                                                          FromPlaceName=p.FromPlaceName,
                                                          ToPlaceName=p.ToPlaceName,
                                                          FlightStartDateTime=p.FlightStartDateTime,
                                                          FlightToDateTime=p.FlightToDateTime,
                                                          TotalBusinessSeats=p.TotalBusinessSeats,
                                                          TotalNonBusinessSeats=p.TotalNonBusinessSeats,
                                                          TicketCost=p.TicketCost,
                                                          FlightSeatRow=p.FlightSeatRow,
                                                          Meal= ((MealEnum)p.Meal).ToString()
                                                       }).ToListAsync();

            var airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList
            {
                airlineFlightDetailsResponsesList = searchList
            };
            return airlineFlightDetailsResponseList;
        }
    }
}
