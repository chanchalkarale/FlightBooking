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
                                                           FlightId=p.Id,
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


        public async Task<bool> BookFlightTicket(FlightBookingDetailsRequest flightBookingDetailsRequest)
        {
            if (flightBookingDetailsRequest == null)
                throw new ArgumentNullException(nameof(flightBookingDetailsRequest));

            bool result = false;
            FlightBookingDetails flightBookingDetails;
            UserBookingDetails userBookingDetails;

            var bookingDetails = await _airlineServiceContext.FlightBookingDetails.Where(d => d.FlightId == flightBookingDetailsRequest.FlightId &&
                                                              d.PNR == flightBookingDetailsRequest.PNR).FirstOrDefaultAsync();
            if(bookingDetails==null)
            {
                flightBookingDetails = new FlightBookingDetails()
                {
                    FlightId=flightBookingDetailsRequest.FlightId,
                    UserId=flightBookingDetailsRequest.UserId,
                    Journey=flightBookingDetailsRequest.Journey,
                    OneWayCost=flightBookingDetailsRequest.OneWayCost,
                    TwoWayCost=flightBookingDetailsRequest.TwoWayCost,
                    TotalBookSeats=flightBookingDetailsRequest.TotalBookSeats,
                    PNR=flightBookingDetailsRequest.PNR,
                    CreateDate=DateTime.Now,
                    Flag=0 
                };
                await _airlineServiceContext.AddAsync(flightBookingDetails);
                await _airlineServiceContext.SaveChangesAsync();
                flightBookingDetailsRequest.FlightBookingId = flightBookingDetails.FlightBookingId;
            }
            else
            {
                bookingDetails.Journey = flightBookingDetailsRequest.Journey;
                bookingDetails.OneWayCost = flightBookingDetailsRequest.OneWayCost;
                bookingDetails.TwoWayCost = flightBookingDetailsRequest.TwoWayCost;
                bookingDetails.TotalBookSeats = flightBookingDetailsRequest.TotalBookSeats;
                bookingDetails.PNR = flightBookingDetailsRequest.PNR;

                await _airlineServiceContext.SaveChangesAsync();
            }

            


            #region add no of seats and No of Users.
            foreach(var userList in flightBookingDetailsRequest.userBookingDetailsRequestList)
            {

                var bookingUser = await _airlineServiceContext.UserBookingDetails.Where(d => d.FlightBookingId == (int)flightBookingDetailsRequest.FlightBookingId
                                                                && d.SeatNumber == userList.SeatNumber).FirstOrDefaultAsync();
                if(bookingUser==null)
                {
                    userBookingDetails = new UserBookingDetails()
                    {
                        FlightBookingId=(int) flightBookingDetailsRequest.FlightBookingId,
                        UserName=userList.UserName,
                        UserEmail=userList.UserEmail,
                        Gender=userList.Gender,
                        Age=userList.Age,
                        Meal=userList.Meal,
                        SeatNumber=userList.SeatNumber,
                        CreateDate=DateTime.Now,
                        Flag=0
                    };

                    await _airlineServiceContext.AddAsync(userBookingDetails);
                }
                else
                {
                    bookingUser.FlightBookingId = (int)flightBookingDetailsRequest.FlightBookingId;
                    bookingUser.UserName = userList.UserName;
                    bookingUser.UserEmail = userList.UserEmail;
                    bookingUser.Gender = userList.Gender;
                    bookingUser.Age = userList.Age;
                    bookingUser.Meal = userList.Meal;
                    bookingUser.SeatNumber = userList.SeatNumber;
                    bookingUser.CreateDate = DateTime.Now;
                    bookingUser.Flag = 0;
                }

                await _airlineServiceContext.SaveChangesAsync();

                result = true;
            }
            #endregion

            return result;
        }

        public async Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr,int userId)
        {
   //         select AFD.FlightNumber,AFD.Airline,UBD.UserName,UBD.UserEmail,UBD.Age,UBD.Gender,UBD.Meal
   // ,UBD.SeatNumber,AFD.FromPlaceName,AFD.ToPlaceName,AFD.FlightStartDateTime,AFD.FlightToDateTime,AFD.TicketCost
   //from[FlightBooking_User].[dbo].[FlightBookingDetails] as FBD
   //inner join[FlightBooking_User].[dbo].[UserBookingDetails] as UBD on FBD.FlightBookingId = UBD.FlightBookingID
   //inner join[FlightBooking_User].[dbo].[AirlineFlightDetails] as AFD on AFD.Id = FBD.FlightId

   //where FBD.PNR = 'QWERDS1234' and FBD.UserId = 1


            if (pnr == null)
                throw new ArgumentNullException(nameof(pnr));

            var ticketBookedDetails = await (from fd in _airlineServiceContext.FlightBookingDetails
                                       join ud in _airlineServiceContext.UserBookingDetails
                                       on fd.FlightBookingId equals ud.FlightBookingId
                                       join ad in _airlineServiceContext.AirlineFlightDetails
                                       on fd.FlightId equals ad.Id
                                       where fd.PNR == pnr && fd.UserId == userId
                                       select new BookedTicketDetailsResponse()
                                       {
                                           FlightNumber = ad.FlightNumber,
                                           Airline=ad.Airline,
                                           Name=ud.UserName,
                                           Email=ud.UserEmail,
                                           Age=ud.Age,
                                           Gender=ud.Gender==0?"Male":"Female",
                                           Meal=((MealEnum)ud.Meal).ToString(),
                                           SeatNo=ud.SeatNumber,
                                           FromPlace=ad.FromPlaceName,
                                           ToPlace=ad.ToPlaceName,
                                           DepartureTime=ad.FlightStartDateTime,
                                           ArrivalTime=ad.FlightToDateTime,
                                           TicketCost=ad.TicketCost
                                       }

                                      ).ToListAsync();
            var booke = new BookedTicketDetailsResponseList
            {

                bookedTicketDetailsResponsesList = ticketBookedDetails
            };
            return booke;
        }
    }
}
