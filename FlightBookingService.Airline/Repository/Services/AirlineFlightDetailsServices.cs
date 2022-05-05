using FlightBookingService.Airline.DTO.Request;
using FlightBookingService.Airline.DTO.Response;
using FlightBookingService.Airline.Models;
using FlightBookingService.Airline.Repository.Interface;
using FlightBookingService.User.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AirlineFlightDetailsServices> _logger;

        #endregion

        #region Controller
        public AirlineFlightDetailsServices(AirlineServiceContext airlineServiceContext,ILogger<AirlineFlightDetailsServices> logger)
        {
            _logger = logger;
            _airlineServiceContext = airlineServiceContext ?? throw new ArgumentNullException(nameof(airlineServiceContext));
        }
        #endregion

        public async Task<bool> AddAirlineDetails(AirlineDetailsRequest airlineDetailsRequest)
        {
            if (airlineDetailsRequest == null)
                throw new ArgumentNullException(nameof(airlineDetailsRequest));

            bool result = false;
            AirlineDetails airlineDetails1;

            var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineNmae == airlineDetailsRequest.AirlineNmae).FirstOrDefaultAsync();
            if (airlineDetails == null)
            {
                airlineDetails1 = new AirlineDetails()
                {
                    AirlineNmae = airlineDetailsRequest.AirlineNmae,
                    Status= 0,
                    CreateDate = DateTime.Now,
                    IsDelete = 0
                };
                await _airlineServiceContext.AddAsync(airlineDetails1);
                result = true;
            }
            else
            { 
                airlineDetails.AirlineNmae = airlineDetailsRequest.AirlineNmae;
                airlineDetails.Status = 0; 
                airlineDetails.CreateDate = DateTime.Now;
                airlineDetails.IsDelete = 0;

                result = true;
            }

            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }



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
                  AirlineId=airlineFlightDetailsRequest.AirlineId,
                  FromPlaceName=airlineFlightDetailsRequest.FromPlaceName,
                  ToPlaceName=airlineFlightDetailsRequest.ToPlaceName,
                  FlightStartDateTime=airlineFlightDetailsRequest.FlightStartDateTime,
                  FlightToDateTime=airlineFlightDetailsRequest.FlightToDateTime,
                  TotalBusinessSeats=airlineFlightDetailsRequest.TotalBusinessSeats,
                  TotalNonBusinessSeats=airlineFlightDetailsRequest.TotalNonBusinessSeats,
                  BusTicketCost = airlineFlightDetailsRequest.BusTicketCost,
                  NonBusTicketCost = airlineFlightDetailsRequest.NonBusTicketCost,
                  FlightSeatRow=airlineFlightDetailsRequest.FlightSeatRow,
                  Meal=airlineFlightDetailsRequest.Meal,
                  CreateDate=DateTime.Now,
                  IsDelete=0
                };
                await _airlineServiceContext.AddAsync(airlineFlightDetails);
                result = true;
            }
            else
            {
                airlineDetails.AirlineId = airlineFlightDetailsRequest.AirlineId;
                airlineDetails.FromPlaceName = airlineFlightDetailsRequest.FromPlaceName;
                airlineDetails.ToPlaceName = airlineFlightDetailsRequest.ToPlaceName;
                airlineDetails.FlightStartDateTime = airlineFlightDetailsRequest.FlightStartDateTime;
                airlineDetails.FlightToDateTime = airlineFlightDetailsRequest.FlightToDateTime;
                airlineDetails.TotalBusinessSeats = airlineFlightDetailsRequest.TotalBusinessSeats;
                airlineDetails.TotalNonBusinessSeats = airlineFlightDetailsRequest.TotalNonBusinessSeats;

                airlineDetails.BusTicketCost = airlineFlightDetailsRequest.BusTicketCost;
                airlineDetails.NonBusTicketCost = airlineFlightDetailsRequest.NonBusTicketCost;
                airlineDetails.FlightSeatRow = airlineFlightDetailsRequest.FlightSeatRow;
                airlineDetails.Meal = airlineFlightDetailsRequest.Meal;
                airlineDetails.CreateDate = DateTime.Now;
                airlineDetails.IsDelete = 0;

                result = true;
            }

            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }

        public async Task<AirlineFlightDetailsResponseList> SearchFlight(string search)
        {
             

        
            var searchList = await _airlineServiceContext.AirlineFlightDetails.Join(_airlineServiceContext.AirlineDetails,
                                                      flight=>flight.AirlineId,
                                                      airline => airline.AirlineId, (flight, airline) => new { 
                                                         airlineNmae=airline.AirlineNmae,
                                                         airlineId=airline.AirlineId,
                                                         fAirlineId=flight.AirlineId,
                                                         FlightNumber=flight.FlightNumber,
                                                          ToPlaceName=flight.ToPlaceName,
                                                          FromPlaceName=flight.FromPlaceName,
                                                          FlightId=flight.Id,
                                                          FlightStartDateTime=flight.FlightStartDateTime,
                                                          FlightToDateTime=flight.FlightToDateTime,
                                                          TotalBusinessSeats = flight.TotalBusinessSeats,
                                                          TotalNonBusinessSeats = flight.TotalNonBusinessSeats,
                                                          BusTicketCost = flight.BusTicketCost,
                                                          NonBusTicketCost = flight.NonBusTicketCost,
                                                          FlightSeatRow = flight.FlightSeatRow,
                                                          Meal = ((MealEnum)flight.Meal).ToString(), 
                                                          status=airline.Status

                                                      }).Where(d => d.status==0 &&d.airlineNmae.Contains(search) ||
                                                       d.FlightNumber.Contains(search) ||
                                                       d.ToPlaceName.Contains(search) ||
                                                       d.FromPlaceName.Contains(search) ||
                                                       d.FlightStartDateTime.ToString().Contains(search)||
                                                       d.FlightToDateTime.ToString().Contains(search)
                                                       ).Select(p=> new AirlineFlightDetailsResponse { 
                                                          FlightId=p.FlightId,
                                                          FlightNumber=p.FlightNumber,
                                                          Airline=p.airlineNmae,
                                                          FromPlaceName=p.FromPlaceName,
                                                          ToPlaceName=p.ToPlaceName,
                                                          FlightStartDateTime=p.FlightStartDateTime,
                                                          FlightToDateTime=p.FlightToDateTime,
                                                          TotalBusinessSeats=p.TotalBusinessSeats,
                                                          TotalNonBusinessSeats=p.TotalNonBusinessSeats,
                                                          BusTicketCost = p.BusTicketCost,
                                                          NonBusTicketCost = p.NonBusTicketCost,
                                                          FlightSeatRow =p.FlightSeatRow,
                                                          Meal= p.Meal
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
            var randomPNR = "";

            var airlineFlightDetails = await _airlineServiceContext.AirlineFlightDetails
                                             .Where(d => d.Id == flightBookingDetailsRequest.FlightId).FirstOrDefaultAsync();

            while (true)
            {
               randomPNR = GetRandomPNR();
               var pnrCheck= await _airlineServiceContext.FlightBookingDetails.Where(d => d.PNR == randomPNR).FirstOrDefaultAsync();
                if(pnrCheck==null)
                {
                    break;
                }
            }

            var bookingDetails = await _airlineServiceContext.FlightBookingDetails.Where(d => d.FlightId == flightBookingDetailsRequest.FlightId &&
                                                              d.PNR == randomPNR).FirstOrDefaultAsync();
            decimal actualCost =flightBookingDetailsRequest.ClassStatus == 0 ? airlineFlightDetails.BusTicketCost : airlineFlightDetails.NonBusTicketCost;
            decimal totalCost = (decimal)CalculateDiscount((Double)actualCost, flightBookingDetailsRequest.DiscountId);

            if (bookingDetails==null)
            {
                flightBookingDetails = new FlightBookingDetails()
                {
                    FlightId = flightBookingDetailsRequest.FlightId,
                    UserId = flightBookingDetailsRequest.UserId,
                    Journey = flightBookingDetailsRequest.Journey,
                    ClassStatus = flightBookingDetailsRequest.ClassStatus,
                    TotalCosts = totalCost,
                    TotalBookSeats = flightBookingDetailsRequest.userBookingDetailsRequestList.Count,
                    PNR=randomPNR,
                    CreateDate=DateTime.Now,
                    IsDelete=0, 
                    DiscountId=flightBookingDetailsRequest.DiscountId
                };
                await _airlineServiceContext.AddAsync(flightBookingDetails);
                await _airlineServiceContext.SaveChangesAsync();
                flightBookingDetailsRequest.FlightBookingId = flightBookingDetails.FlightBookingId;
            }
            else
            {
                bookingDetails.Journey = flightBookingDetailsRequest.Journey;
                bookingDetails.ClassStatus = flightBookingDetailsRequest.ClassStatus;
                bookingDetails.TotalCosts = totalCost;
                bookingDetails.TotalBookSeats = flightBookingDetailsRequest.userBookingDetailsRequestList.Count;
                bookingDetails.PNR = randomPNR;
                bookingDetails.DiscountId = flightBookingDetailsRequest.DiscountId;

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

        public async Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr)
        {
           //select AFD.FlightNumber,AFD.Airline,UBD.UserName,UBD.UserEmail,UBD.Age,UBD.Gender,UBD.Meal
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
                                             join a in _airlineServiceContext.AirlineDetails
                                                  on ad.AirlineId equals a.AirlineId
                                             where fd.PNR == pnr && fd.IsDelete == 0//&& fd.UserId == userId
                                       select new BookedTicketDetailsResponse()
                                       {
                                           FlightNumber = ad.FlightNumber,
                                           AirlineName=a.AirlineNmae,
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
                                           BusTicketCost=ad.BusTicketCost,
                                           NonBusTicketCost=ad.NonBusTicketCost,
                                           Journey= fd.Journey==0?"One Way":"Two Way"
                                       }

                                      ).ToListAsync();
            var booke = new BookedTicketDetailsResponseList
            {

                bookedTicketDetailsResponsesList = ticketBookedDetails
            };
            return booke;
        }

        public async Task<bool> CancleBookTicket(string pnr)
        {
            bool result = false;
            var bookingUser = await _airlineServiceContext.FlightBookingDetails.Where(d => d.PNR ==  pnr).FirstOrDefaultAsync();
            if(bookingUser!=null)
            {
                bookingUser.IsDelete = 1;
                result = true;
            }
            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }

        public async Task<BookedTicketDetailsResponseList> GetBookedTicketHistory(string emailId)
        {
            //         select AFD.FlightNumber,AFD.Airline,UBD.UserName,UBD.UserEmail,UBD.Age,UBD.Gender,UBD.Meal
            // ,UBD.SeatNumber,AFD.FromPlaceName,AFD.ToPlaceName,AFD.FlightStartDateTime,AFD.FlightToDateTime,AFD.TicketCost
            //from[FlightBooking_User].[dbo].[FlightBookingDetails] as FBD
            //inner join[FlightBooking_User].[dbo].[UserBookingDetails] as UBD on FBD.FlightBookingId = UBD.FlightBookingID
            //inner join[FlightBooking_User].[dbo].[AirlineFlightDetails] as AFD on AFD.Id = FBD.FlightId

            //where FBD.PNR = 'QWERDS1234' and FBD.UserId = 1


            if (emailId == null)
                throw new ArgumentNullException(nameof(emailId));

            var ticketBookedDetails = await (from fd in _airlineServiceContext.FlightBookingDetails
                                             join ud in _airlineServiceContext.UserBookingDetails
                                             on fd.FlightBookingId equals ud.FlightBookingId
                                             join ad in _airlineServiceContext.AirlineFlightDetails
                                             on fd.FlightId equals ad.Id
                                             join a in _airlineServiceContext.AirlineDetails
                                             on ad.AirlineId equals a.AirlineId
                                             where ud.UserEmail == emailId //&& fd.UserId == userId
                                             select new BookedTicketDetailsResponse()
                                             {
                                                 FlightNumber = ad.FlightNumber,
                                                 AirlineName = a.AirlineNmae,
                                                 Name = ud.UserName,
                                                 Email = ud.UserEmail,
                                                 Age = ud.Age,
                                                 Gender = ud.Gender == 0 ? "Male" : "Female",
                                                 Meal = ((MealEnum)ud.Meal).ToString(),
                                                 SeatNo = ud.SeatNumber,
                                                 FromPlace = ad.FromPlaceName,
                                                 ToPlace = ad.ToPlaceName,
                                                 DepartureTime = ad.FlightStartDateTime,
                                                 ArrivalTime = ad.FlightToDateTime,
                                                 BusTicketCost = ad.BusTicketCost,
                                                 NonBusTicketCost = ad.NonBusTicketCost
                                             }

                                      ).ToListAsync();
            var booke = new BookedTicketDetailsResponseList
            {

                bookedTicketDetailsResponsesList = ticketBookedDetails
            };
            return booke;
        }

        public async Task<bool> UpdateAirline(int airlineId,int status)
        {
            bool result = false;
            var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineId == airlineId).FirstOrDefaultAsync();
            if (airlineDetails != null)
            {
                airlineDetails.Status = status;// 1= blocked ariline ,0=unblockedAirline 
                await _airlineServiceContext.SaveChangesAsync();
                result = true;
            }

           
           return result;
        }

        public async Task<bool> DeleteAirline(int airlineId)
        {
            bool result = false;
            var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineId == airlineId).FirstOrDefaultAsync();
            if (airlineDetails != null)
            {
                airlineDetails.IsDelete = 1;// 1= Deleted ariline ,0= Available Airline 
                await _airlineServiceContext.SaveChangesAsync();
                result = true;
            }


            return result;
        }

        public async Task<bool> AddDiscount(DiscountsRequest discountsRequest)
        {
            if (discountsRequest == null)
                throw new ArgumentNullException(nameof(discountsRequest));

            bool result = false;
            Discount discount;

            var discountsDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountCode == discountsRequest.DiscountCode).FirstOrDefaultAsync();
            if (discountsDetails == null)
            {
                discount = new Discount()
                {
                    DiscountCode = discountsRequest.DiscountCode,
                    DiscountCost = discountsRequest.DiscountCost,
                    ExpiryDate=DateTime.Now,
                    IsDelete=0,
                    CreateDate=DateTime.Now
                };
                await _airlineServiceContext.AddAsync(discount);
                result = true;
            }
            else
            {
                discountsDetails.DiscountCode = discountsRequest.DiscountCode;
                discountsDetails.DiscountCost = discountsRequest.DiscountCost;
                discountsDetails.ExpiryDate = DateTime.Now;
                result = true;
            }

            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateDiscount(DiscountsRequest discountsRequest)
        {
            if (discountsRequest == null)
                throw new ArgumentNullException(nameof(discountsRequest));

            bool result = false; 

            var discountsDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountsRequest.DiscountId).FirstOrDefaultAsync();
            if (discountsDetails != null)
            { 
                discountsDetails.DiscountCode = discountsRequest.DiscountCode;
                discountsDetails.DiscountCost = discountsRequest.DiscountCost;
                discountsDetails.ExpiryDate = discountsRequest.ExpiryDate;
                result = true;
            }

            await _airlineServiceContext.SaveChangesAsync();
            return result;
        }

        public async Task<DiscountsResponseList> GetAllDiscounts()
        { 

            var discountListDetails = await (from fd in _airlineServiceContext.Discounts
                                             where(fd.IsDelete==0)
                                             select new DiscountsResponse()
                                             {
                                                 DiscountCode = fd.DiscountCode,
                                                 DiscountCost = fd.DiscountCost,
                                                 DiscountId=fd.DiscountId,
                                                 ExpiryDate=fd.ExpiryDate
                                             }

                                      ).ToListAsync();
            var discountList = new DiscountsResponseList
            {

                DiscountsResponsesLists = discountListDetails
            };
            return discountList;
        }

        public async Task<List<GetAirlineResponse>> GetAirlines()
        {

            var airlineListDetails = await (from fd in _airlineServiceContext.AirlineDetails
                                            where fd.IsDelete==0 && fd.Status==0
                                             select new GetAirlineResponse()
                                             {
                                                 AirlineId = fd.AirlineId,
                                                 AirlineName = fd.AirlineNmae,
                                                 Status=fd.Status
                                             }

                                      ).ToListAsync();
           
            return airlineListDetails;
        }

        public async Task<List<GetAirlineResponse>> GetAllAirlines()
        {

            var airlineListDetails = await (from fd in _airlineServiceContext.AirlineDetails
                                            where fd.IsDelete == 0
                                            select new GetAirlineResponse()
                                            {
                                                AirlineId = fd.AirlineId,
                                                AirlineName = fd.AirlineNmae,
                                                Status = fd.Status
                                            }

                                      ).ToListAsync();

            return airlineListDetails;
        }

        public async Task<AirlineFlightDetailsResponseList> GetAllAirlineFlightsDetails()
        {



            var searchList = await _airlineServiceContext.AirlineFlightDetails.Join(_airlineServiceContext.AirlineDetails,
                                                      flight => flight.AirlineId,
                                                      airline => airline.AirlineId, (flight, airline) => new {
                                                          airlineNmae = airline.AirlineNmae,
                                                          airlineId = airline.AirlineId,
                                                          fAirlineId = flight.AirlineId,
                                                          FlightNumber = flight.FlightNumber,
                                                          ToPlaceName = flight.ToPlaceName,
                                                          FromPlaceName = flight.FromPlaceName,
                                                          FlightId = flight.Id,
                                                          FlightStartDateTime = flight.FlightStartDateTime,
                                                          FlightToDateTime = flight.FlightToDateTime,
                                                          TotalBusinessSeats = flight.TotalBusinessSeats,
                                                          TotalNonBusinessSeats = flight.TotalNonBusinessSeats,
                                                          BusTicketCost = flight.BusTicketCost,
                                                          NonBusTicketCost = flight.NonBusTicketCost,
                                                          FlightSeatRow = flight.FlightSeatRow,
                                                          Meal = ((MealEnum)flight.Meal).ToString(),
                                                          status = airline.Status,
                                                          isDeletedflight=flight.IsDelete

                                                      }).Where(d => d.status == 0&& d.isDeletedflight==0)
                                                      .Select(p => new AirlineFlightDetailsResponse
                                                       {
                                                           FlightId = p.FlightId,
                                                           FlightNumber = p.FlightNumber,
                                                           Airline = p.airlineNmae,
                                                           FromPlaceName = p.FromPlaceName,
                                                           ToPlaceName = p.ToPlaceName,
                                                           FlightStartDateTime = p.FlightStartDateTime,
                                                           FlightToDateTime = p.FlightToDateTime,
                                                           TotalBusinessSeats = p.TotalBusinessSeats,
                                                           TotalNonBusinessSeats = p.TotalNonBusinessSeats,
                                                           BusTicketCost = p.BusTicketCost,
                                                           NonBusTicketCost = p.NonBusTicketCost,
                                                           FlightSeatRow = p.FlightSeatRow,
                                                           Meal = p.Meal
                                                       }).ToListAsync();

            var airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList
            {
                airlineFlightDetailsResponsesList = searchList
            };
            return airlineFlightDetailsResponseList;
        }


        #region Private Method

        private static Random random = new Random();

        private static string GetRandomPNR()
        {
            int length = 12;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private double CalculateDiscount(double actualCost,int discountId)
        {
            double totalCost=0;
            var discountCost = _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountId).Select(s => s.DiscountCost).FirstOrDefault();

            totalCost = actualCost - discountCost;
            return totalCost;
        }

        public async Task<bool> RemoveAirlineFlight(int flightId)
        {
            bool result = false;
            var flightDetails = await _airlineServiceContext.AirlineFlightDetails.Where(d => d.Id == flightId).FirstOrDefaultAsync();
            if (flightDetails != null)
            {
                flightDetails.IsDelete = 1;// 1= delete flight ,0= available 
                await _airlineServiceContext.SaveChangesAsync();
                result = true;
            }


            return result;
        }

        public async Task<bool> RemoveDiscount(int discountId)
        {
            bool result = false;
            var flightDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountId).FirstOrDefaultAsync();
            if (flightDetails != null)
            {
                flightDetails.IsDelete = 1;// 1= delete discount ,0= available 
                await _airlineServiceContext.SaveChangesAsync();
                result = true;
            }


            return result;
        }


        #endregion
    }
}
