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

        #region define private 
        private readonly AirlineServiceContext _airlineServiceContext;
        private readonly ILogger<AirlineFlightDetailsServices> _logger;

        #endregion

        #region Constructor
        public AirlineFlightDetailsServices(AirlineServiceContext airlineServiceContext,ILogger<AirlineFlightDetailsServices> logger)
        {
            _logger = logger;
            _airlineServiceContext = airlineServiceContext ?? throw new ArgumentNullException(nameof(airlineServiceContext));
        }
        #endregion

        #region Public Methods

        public async Task<bool> AddAirlineDetails(AirlineDetailsRequest airlineDetailsRequest)
        {
            _logger.LogInformation("Inside add airline details fun");
            #region define
            bool result = false;
            AirlineDetails airlineDetails1;

            #endregion
            try
            {
                if (airlineDetailsRequest == null)
                    throw new ArgumentNullException(nameof(airlineDetailsRequest));

                var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineNmae == airlineDetailsRequest.AirlineNmae).FirstOrDefaultAsync();
                if (airlineDetails == null)
                {
                    airlineDetails1 = new AirlineDetails()
                    {
                        AirlineNmae = airlineDetailsRequest.AirlineNmae,
                        Status = 0,
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
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }
            return result;
        }



        public async Task<bool> AddAirlineSchedule(AirlineFlightDetailsRequest airlineFlightDetailsRequest)
        {

            #region define

            bool result = false;
            AirlineFlightDetails airlineFlightDetails;

            #endregion

            try
            {

                if (airlineFlightDetailsRequest == null)
                    throw new ArgumentNullException(nameof(airlineFlightDetailsRequest));



                var airlineDetails = await _airlineServiceContext.AirlineFlightDetails.Where(d => d.FlightNumber == airlineFlightDetailsRequest.FlightNumber).FirstOrDefaultAsync();
                if (airlineDetails == null)
                {
                    airlineFlightDetails = new AirlineFlightDetails()
                    {
                        FlightNumber = airlineFlightDetailsRequest.FlightNumber,
                        AirlineId = airlineFlightDetailsRequest.AirlineId,
                        FromPlaceName = airlineFlightDetailsRequest.FromPlaceName,
                        ToPlaceName = airlineFlightDetailsRequest.ToPlaceName,
                        FlightStartDateTime = airlineFlightDetailsRequest.FlightStartDateTime,
                        FlightToDateTime = airlineFlightDetailsRequest.FlightToDateTime,
                        TotalBusinessSeats = airlineFlightDetailsRequest.TotalBusinessSeats,
                        TotalNonBusinessSeats = airlineFlightDetailsRequest.TotalNonBusinessSeats,
                        BusTicketCost = airlineFlightDetailsRequest.BusTicketCost,
                        NonBusTicketCost = airlineFlightDetailsRequest.NonBusTicketCost,
                        FlightSeatRow = airlineFlightDetailsRequest.FlightSeatRow,
                        Meal = airlineFlightDetailsRequest.Meal,
                        CreateDate = DateTime.Now,
                        IsDelete = 0
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
            }
            catch(Exception ex)
            {
                 result = false;
                _logger.LogError(ex.Message.ToString());
            }
            return result;
        }

        public async Task<AirlineFlightDetailsResponseList> SearchFlight(string search)
        {
            #region define
            var searchList =new List<AirlineFlightDetailsResponse>();
            var airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList();

            #endregion

            try
            {
                searchList = await _airlineServiceContext.AirlineFlightDetails.Join(_airlineServiceContext.AirlineDetails,
                                                          flight => flight.AirlineId,
                                                          airline => airline.AirlineId, (flight, airline) => new
                                                          {
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
                                                              status = airline.Status

                                                          }).Where(d => d.status == 0 && d.airlineNmae.Contains(search) ||
                                                           d.FlightNumber.Contains(search) ||
                                                           d.ToPlaceName.Contains(search) ||
                                                           d.FromPlaceName.Contains(search) ||
                                                           d.FlightStartDateTime.ToString().Contains(search) ||
                                                           d.FlightToDateTime.ToString().Contains(search)
                                                           ).Select(p => new AirlineFlightDetailsResponse
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

                 airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList
                {
                    airlineFlightDetailsResponsesList = searchList
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
            }
            return airlineFlightDetailsResponseList;
        }


        public async Task<bool> BookFlightTicket(FlightBookingDetailsRequest flightBookingDetailsRequest)
        {

            #region define
                    bool result = false;
                    FlightBookingDetails flightBookingDetails;
                    UserBookingDetails userBookingDetails;
                    var randomPNR = "";
            #endregion


            try
            {
                if (flightBookingDetailsRequest == null)
                    throw new ArgumentNullException(nameof(flightBookingDetailsRequest));



                var airlineFlightDetails = await _airlineServiceContext.AirlineFlightDetails
                                                 .Where(d => d.Id == flightBookingDetailsRequest.FlightId).FirstOrDefaultAsync();

                while (true)
                {
                    randomPNR = GetRandomPNR();
                    var pnrCheck = await _airlineServiceContext.FlightBookingDetails.Where(d => d.PNR == randomPNR).FirstOrDefaultAsync();
                    if (pnrCheck == null)
                    {
                        break;
                    }
                }

                var bookingDetails = await _airlineServiceContext.FlightBookingDetails.Where(d => d.FlightId == flightBookingDetailsRequest.FlightId &&
                                                                  d.PNR == randomPNR).FirstOrDefaultAsync();
                decimal actualCost = flightBookingDetailsRequest.ClassStatus == 0 ? airlineFlightDetails.BusTicketCost : airlineFlightDetails.NonBusTicketCost;
                decimal totalCost = (decimal)CalculateDiscount((Double)actualCost * flightBookingDetailsRequest.userBookingDetailsRequestList.Count, flightBookingDetailsRequest.DiscountId);

                if (bookingDetails == null)
                {
                    flightBookingDetails = new FlightBookingDetails()
                    {
                        FlightId = flightBookingDetailsRequest.FlightId,
                        UserId = flightBookingDetailsRequest.UserId,
                        Journey = flightBookingDetailsRequest.Journey,
                        ClassStatus = flightBookingDetailsRequest.ClassStatus,
                        TotalCosts = totalCost,
                        TotalBookSeats = flightBookingDetailsRequest.userBookingDetailsRequestList.Count,
                        PNR = randomPNR,
                        CreateDate = DateTime.Now,
                        IsDelete = 0,
                        DiscountId = flightBookingDetailsRequest.DiscountId
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
                foreach (var userList in flightBookingDetailsRequest.userBookingDetailsRequestList)
                {
                    var bookingUser = await _airlineServiceContext.UserBookingDetails.Where(d => d.FlightBookingId == (int)flightBookingDetailsRequest.FlightBookingId
                                                                    && d.SeatNumber == userList.SeatNumber).FirstOrDefaultAsync();
                    if (bookingUser == null)
                    {
                        userBookingDetails = new UserBookingDetails()
                        {
                            FlightBookingId = (int)flightBookingDetailsRequest.FlightBookingId,
                            UserName = userList.UserName,
                            UserEmail = userList.UserEmail,
                            Gender = userList.Gender,
                            Age = userList.Age,
                            Meal = userList.Meal,
                            SeatNumber = userList.SeatNumber,
                            CreateDate = DateTime.Now,
                            Flag = 0
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
            }
            catch(Exception ex)
            {
                result = false;
                _logger.LogError("Booking method error : " + ex.Message.ToString());
            }
            return result;
        }

        public async Task<BookedTicketDetailsResponseList> GetBookedTicketDetails(string pnr)
        {
            #region

            var bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList();

            #endregion

            try
            {

                if (pnr == null)
                    throw new ArgumentNullException(nameof(pnr));

                var ticketBookedDetails = await (from fd in _airlineServiceContext.FlightBookingDetails
                                                 join ud in _airlineServiceContext.UserBookingDetails
                                                 on fd.FlightBookingId equals ud.FlightBookingId
                                                 join ad in _airlineServiceContext.AirlineFlightDetails
                                                 on fd.FlightId equals ad.Id
                                                 join a in _airlineServiceContext.AirlineDetails
                                                      on ad.AirlineId equals a.AirlineId
                                                 where fd.PNR == pnr //&& fd.IsDelete == 0//&& fd.UserId == userId
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
                                                     NonBusTicketCost = ad.NonBusTicketCost,
                                                     Journey = fd.Journey == 0 ? "One Way" : "Two Way",
                                                     ClassStatus = fd.ClassStatus,
                                                     PnrNumber = fd.PNR
                                                 }

                                          ).ToListAsync();
                bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList
                {

                    bookedTicketDetailsResponsesList = ticketBookedDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetBookedTicketDetails Error :  " + ex.Message.ToString());
            }
            return bookedTicketDetailsResponseList;
        }

        public async Task<bool> CancleBookTicket(string pnr)
        {
            #region define
               bool result = false;
            #endregion

            try
            {
                var bookingUser = await _airlineServiceContext.FlightBookingDetails.Where(d => d.PNR == pnr).FirstOrDefaultAsync();
                if (bookingUser != null)
                {
                    bookingUser.IsDelete = 1;
                    result = true;
                }
                await _airlineServiceContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                 result = false;
                _logger.LogError("Cancle Booked ticket error : " + ex.Message.ToString());
            }
            return result;
        }


        public async Task<BookedTicketDetailsResponseList> GetBookedTicketHistory(string emailId)
        {

            #region define
            var bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList();
            #endregion

            try
            {
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
                bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList
                {

                    bookedTicketDetailsResponsesList = ticketBookedDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetBookedTicketHistory error : " + ex.Message.ToString());
            }
            return bookedTicketDetailsResponseList;
        }

        public async Task<bool> UpdateAirline(int airlineId,int status)
        {
            #region define
            bool result = false;
            #endregion

            try
            {
                var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineId == airlineId).FirstOrDefaultAsync();
                if (airlineDetails != null)
                {
                    airlineDetails.Status = status;// 1= blocked ariline ,0=unblockedAirline 
                    await _airlineServiceContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                result = false;
                _logger.LogError("UpdateAIrline Error : " + ex.Message.ToString());
            } 
           return result;
        }

        public async Task<bool> DeleteAirline(int airlineId)
        {
            #region define
            bool result = false;
            #endregion

            try
            {
                var airlineDetails = await _airlineServiceContext.AirlineDetails.Where(d => d.AirlineId == airlineId).FirstOrDefaultAsync();
            if (airlineDetails != null)
            {
                airlineDetails.IsDelete = 1;// 1= Deleted ariline ,0= Available Airline 
                await _airlineServiceContext.SaveChangesAsync();
                result = true;
            }
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogError("DeleteAirline Error : " + ex.Message.ToString());
            }

            return result;
        }

        public async Task<bool> AddDiscount(DiscountsRequest discountsRequest)
        {

            #region define
                bool result = false;
                Discount discount;
            #endregion

            try
            {
                if (discountsRequest == null)
                    throw new ArgumentNullException(nameof(discountsRequest));



                var discountsDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountCode == discountsRequest.DiscountCode).FirstOrDefaultAsync();
                if (discountsDetails == null)
                {
                    discount = new Discount()
                    {
                        DiscountCode = discountsRequest.DiscountCode,
                        DiscountCost = discountsRequest.DiscountCost,
                        ExpiryDate = discountsRequest.ExpiryDate.AddDays(1),
                        IsDelete = 0,
                        CreateDate = DateTime.Now
                    };
                    await _airlineServiceContext.AddAsync(discount);
                    result = true;
                }
                else
                {
                    discountsDetails.DiscountCode = discountsRequest.DiscountCode;
                    discountsDetails.DiscountCost = discountsRequest.DiscountCost;
                    discountsDetails.ExpiryDate = discountsRequest.ExpiryDate.AddDays(1);
                    discountsDetails.IsDelete = 0;
                    discountsDetails.CreateDate = DateTime.Now;
                    result = true;
                }

                await _airlineServiceContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                result = false;
                _logger.LogError("AddDiscount Error : " + ex.Message.ToString());
            }
            return result;
        }

        public async Task<bool> UpdateDiscount(DiscountsRequest discountsRequest)
        {
            #region define
            bool result = false;
            #endregion

            try
            {
                if (discountsRequest == null)
                    throw new ArgumentNullException(nameof(discountsRequest));



                var discountsDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountsRequest.DiscountId).FirstOrDefaultAsync();
                if (discountsDetails != null)
                {
                    discountsDetails.DiscountCode = discountsRequest.DiscountCode;
                    discountsDetails.DiscountCost = discountsRequest.DiscountCost;
                    discountsDetails.ExpiryDate = discountsRequest.ExpiryDate.AddDays(1);
                    result = true;
                }

                await _airlineServiceContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                 result = false;
                _logger.LogError("UpdateDiscount Error : " + ex.Message.ToString());
            }
            return result;
        }

        public async Task<DiscountsResponseList> GetAllDiscounts()
        {
            #region define
            var discountList = new DiscountsResponseList();
            #endregion

            try
            {
                var discountListDetails = await (from fd in _airlineServiceContext.Discounts
                                                 where (fd.IsDelete == 0)
                                                 select new DiscountsResponse()
                                                 {
                                                     DiscountCode = fd.DiscountCode,
                                                     DiscountCost = fd.DiscountCost,
                                                     DiscountId = fd.DiscountId,
                                                     ExpiryDate = fd.ExpiryDate
                                                 }

                                          ).ToListAsync();
                discountList = new DiscountsResponseList
                {

                    DiscountsResponsesLists = discountListDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetAllDiscount Error : " + ex.Message.ToString());
            }
            return discountList;
        }

        public async Task<List<GetAirlineResponse>> GetAirlines()
        {
            #region define 
                    var airlineListDetails = new List<GetAirlineResponse>();
            #endregion

            try
            {


                airlineListDetails = await (from fd in _airlineServiceContext.AirlineDetails
                                            where fd.IsDelete == 0 && fd.Status == 0
                                            select new GetAirlineResponse()
                                            {
                                                AirlineId = fd.AirlineId,
                                                AirlineName = fd.AirlineNmae,
                                                Status = fd.Status
                                            }

                                          ).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("GetAirlines Error : " + ex.Message.ToString());
            }
           
            return airlineListDetails;
        }

        public async Task<List<GetAirlineResponse>> GetAllAirlines()
        {
            #region define 
            var airlineListDetails = new List<GetAirlineResponse>();
            #endregion

            try
            {
                airlineListDetails = await (from fd in _airlineServiceContext.AirlineDetails
                                            where fd.IsDelete == 0
                                            select new GetAirlineResponse()
                                            {
                                                AirlineId = fd.AirlineId,
                                                AirlineName = fd.AirlineNmae,
                                                Status = fd.Status
                                            }

                                      ).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAirlines Error : " + ex.Message.ToString());
            }

            return airlineListDetails;
        }

        public async Task<AirlineFlightDetailsResponseList> GetAllAirlineFlightsDetails()
        {
            #region define
            var airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList();
            var searchList = new List<AirlineFlightDetailsResponse>();
            #endregion

            try
            {

                searchList = await _airlineServiceContext.AirlineFlightDetails.Join(_airlineServiceContext.AirlineDetails,
                                                         flight => flight.AirlineId,
                                                         airline => airline.AirlineId, (flight, airline) => new
                                                         {
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
                                                             isDeletedflight = flight.IsDelete

                                                         }).Where(d => d.status == 0 && d.isDeletedflight == 0)
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

                airlineFlightDetailsResponseList = new AirlineFlightDetailsResponseList
                {
                    airlineFlightDetailsResponsesList = searchList
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetAllAirlineFlightDetails Error : " + ex.Message.ToString());
            }
            return airlineFlightDetailsResponseList;
        }


        

        public async Task<bool> RemoveAirlineFlight(int flightId)
        {
            #region define
                bool result = false;
            #endregion
            try
            {
                var flightDetails = await _airlineServiceContext.AirlineFlightDetails.Where(d => d.Id == flightId).FirstOrDefaultAsync();
                if (flightDetails != null)
                {
                    flightDetails.IsDelete = 1;// 1= delete flight ,0= available 
                    await _airlineServiceContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                result = false;
                _logger.LogError("RemoveAirlineFlight Error : " + ex.Message.ToString());
            }


            return result;
        }

        public async Task<bool> RemoveDiscount(int discountId)
        {
            #region define
            bool result = false;
            #endregion
            try
            {
                var flightDetails = await _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountId).FirstOrDefaultAsync();
                if (flightDetails != null)
                {
                    flightDetails.IsDelete = 1;// 1= delete discount ,0= available 
                    await _airlineServiceContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogError("RemoveDiscount Error : " + ex.Message.ToString());
            }

            return result;
        }

        public async Task<List<AirlineFlightDetailsRawQueryModel>> SearchFlights(SearchFlightRequest searchFlightRequest)
        {
            #region define 
               var query = "";
            var searchResult = new List<AirlineFlightDetailsRawQueryModel>();
            #endregion

            try
            {
                if (searchFlightRequest.seatsClass == "1")//Economy class
                {

                    query = "SELECT af.Id as 'FlightId',af.FlightNumber,AD.AirlineNmae as Airline," +
                                            " AF.FromPlaceName,AF.ToPlaceName,AF.FlightStartDateTime," +
                                            " AF.FlightToDateTime,AF.NonBusTicketCost as TicketCost," +
                                            " AF.TotalBusinessSeats,AF.TotalNonBusinessSeats,AF.FlightSeatRow," +
                                            " AF.Meal , '1' as ClassStatus FROM [dbo].[AirlineFlightDetails] as AF " +
                                            " inner join [dbo].[AirlineDetails] as AD on AF.AirlineId = AD.AirlineId ";
                }
                else
                {
                    query = "SELECT af.Id as 'FlightId',af.FlightNumber,AD.AirlineNmae as Airline," +
                                            " AF.FromPlaceName,AF.ToPlaceName,AF.FlightStartDateTime," +
                                            " AF.FlightToDateTime,AF.BusTicketCost as TicketCost," +
                                            " AF.TotalBusinessSeats,AF.TotalNonBusinessSeats,AF.FlightSeatRow," +
                                            " AF.Meal , '2' as ClassStatus   FROM [dbo].[AirlineFlightDetails] as AF " +
                                            " inner join [dbo].[AirlineDetails] as AD on AF.AirlineId = AD.AirlineId ";
                }


                query += " where (FromPlaceName='" + searchFlightRequest.FromPlace + "' and ToPlaceName='" + searchFlightRequest.ToPlace + "' " +
                    " and convert(date,FlightStartDateTime,103) =convert(date,'" + searchFlightRequest.DepartureDate.AddDays(1) + "',103))";

                if (searchFlightRequest.JourneyType == "1")
                {
                    query += " or (FromPlaceName='" + searchFlightRequest.ToPlace + "' and ToPlaceName='" + searchFlightRequest.FromPlace + "' " +
                    " and convert(date,FlightStartDateTime,103) = convert(date,'" + searchFlightRequest.ReturnDate.AddDays(1) + "',103))";
                }

                searchResult = await _airlineServiceContext.airlineFlightDetailsRawQueryModels.FromSqlRaw(query).ToListAsync();
                //var searchResult = await from af in _airlineServiceContext.AirlineFlightDetails
                //                          join ad in _airlineServiceContext.AirlineDetails
                //                          on af.AirlineId equals ad.AirlineId into g
                //                          from gm in g.DefaultIfEmpty()
                //                              //where ad.Status==0
                //                         select new AirlineFlightDetailsResponse()
                //                         {
                //                             FlightId = af.Id,
                //                             FlightNumber = af.FlightNumber,
                //                            // Airline = ad.AirlineNmae,
                //                             FromPlaceName = af.FromPlaceName,
                //                             ToPlaceName = af.ToPlaceName,
                //                             FlightStartDateTime = af.FlightStartDateTime,
                //                             FlightToDateTime = af.FlightToDateTime,
                //                             TotalBusinessSeats = af.TotalBusinessSeats,
                //                             TotalNonBusinessSeats = af.TotalNonBusinessSeats,
                //                             BusTicketCost = af.BusTicketCost,
                //                             NonBusTicketCost = af.NonBusTicketCost,
                //                             FlightSeatRow = af.FlightSeatRow,
                //                             Meal = ((MealEnum)af.Meal).ToString()

                //                         };

                //var searchList = await _airlineServiceContext.AirlineFlightDetails.Join(_airlineServiceContext.AirlineDetails,
                //                                          flight => flight.AirlineId,
                //                                          airline => airline.AirlineId, (flight, airline) => new {
                //                                              airlineNmae = airline.AirlineNmae,
                //                                              airlineId = airline.AirlineId,
                //                                              fAirlineId = flight.AirlineId,
                //                                              FlightNumber = flight.FlightNumber,
                //                                              ToPlaceName = flight.ToPlaceName,
                //                                              FromPlaceName = flight.FromPlaceName,
                //                                              FlightId = flight.Id,
                //                                              FlightStartDateTime = flight.FlightStartDateTime,
                //                                              FlightToDateTime = flight.FlightToDateTime,
                //                                              TotalBusinessSeats = flight.TotalBusinessSeats,
                //                                              TotalNonBusinessSeats = flight.TotalNonBusinessSeats,
                //                                              BusTicketCost = flight.BusTicketCost,
                //                                              NonBusTicketCost = flight.NonBusTicketCost,
                //                                              FlightSeatRow = flight.FlightSeatRow,
                //                                              Meal = ((MealEnum)flight.Meal).ToString(),
                //                                              status = airline.Status

                //                                          }).Where(d => d.status == 0 && d.airlineNmae.Contains(search) ||
                //                                           d.FlightNumber.Contains(search) ||
                //                                           d.ToPlaceName.Contains(search) ||
                //                                           d.FromPlaceName.Contains(search) ||
                //                                           d.FlightStartDateTime.ToString().Contains(search) ||
                //                                           d.FlightToDateTime.ToString().Contains(search)
                //                                           ).Select(p => new AirlineFlightDetailsResponse
                //                                           {
                //                                               FlightId = p.FlightId,
                //                                               FlightNumber = p.FlightNumber,
                //                                               Airline = p.airlineNmae,
                //                                               FromPlaceName = p.FromPlaceName,
                //                                               ToPlaceName = p.ToPlaceName,
                //                                               FlightStartDateTime = p.FlightStartDateTime,
                //                                               FlightToDateTime = p.FlightToDateTime,
                //                                               TotalBusinessSeats = p.TotalBusinessSeats,
                //                                               TotalNonBusinessSeats = p.TotalNonBusinessSeats,
                //                                               BusTicketCost = p.BusTicketCost,
                //                                               NonBusTicketCost = p.NonBusTicketCost,
                //                                               FlightSeatRow = p.FlightSeatRow,
                //                                               Meal = p.Meal
                //                                           }).ToListAsync();

            }
            catch(Exception ex)
            {
                _logger.LogError("SearchFlights Error : " + ex.Message.ToString());
            }
            return searchResult;
        }


        public async Task<DiscountsResponseList> GetDiscountUsingCode(string discountCode)
        {
            #region define
               var discountList = new DiscountsResponseList();
            var discountListDetails = new List<DiscountsResponse>();

            #endregion

            try
            {
                discountListDetails = await (from fd in _airlineServiceContext.Discounts
                                             where (fd.IsDelete == 0 && fd.DiscountCode == discountCode)
                                             select new DiscountsResponse()
                                             {
                                                 DiscountCode = fd.DiscountCode,
                                                 DiscountCost = fd.DiscountCost,
                                                 DiscountId = fd.DiscountId,
                                                 ExpiryDate = fd.ExpiryDate
                                             }

                                         ).ToListAsync();
                discountList = new DiscountsResponseList
                {

                    DiscountsResponsesLists = discountListDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetDiscountUsingCode Error : " + ex.Message.ToString());
            }
            return discountList;
        }


        public async Task<BookedTicketDetailsResponseList> GetAllBookedTicket(int userId)
        {
            #region define

            var bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList();
            var ticketBookedDetails = new List<BookedTicketDetailsResponse>();
            #endregion

            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId));

                ticketBookedDetails = await (from fd in _airlineServiceContext.FlightBookingDetails 
                                             join ad in _airlineServiceContext.AirlineFlightDetails
                                             on fd.FlightId equals ad.Id
                                             join a in _airlineServiceContext.AirlineDetails
                                                  on ad.AirlineId equals a.AirlineId
                                             where fd.IsDelete == 0 && fd.UserId == userId
                                             select new BookedTicketDetailsResponse()
                                             {
                                                 FlightNumber = ad.FlightNumber,
                                                 AirlineName = a.AirlineNmae, 
                                                 FromPlace = ad.FromPlaceName,
                                                 ToPlace = ad.ToPlaceName,
                                                 DepartureTime = ad.FlightStartDateTime,
                                                 ArrivalTime = ad.FlightToDateTime,
                                                 TotalCost = fd.TotalCosts,
                                                 Journey = fd.Journey == 0 ? "One Way" : "Two Way",
                                                 ClassStatus = fd.ClassStatus,
                                                 CancalStatus = ad.FlightStartDateTime <= DateTime.Now ? false : true,
                                                 PnrNumber = fd.PNR,
                                                 TotalSeats = fd.TotalBookSeats
                                             }

                                         ).ToListAsync();
                bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList
                {

                    bookedTicketDetailsResponsesList = ticketBookedDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetAllBookedTicket Error : " + ex.Message.ToString());
            }
            return bookedTicketDetailsResponseList;
        }


        public async Task<BookedTicketDetailsResponseList> GetBookedTicketHistoryViaUserId(int userId)
        { 

            #region define
            var ticketBookedDetails = new List<BookedTicketDetailsResponse>();
            var bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList();
            #endregion

            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId));

                ticketBookedDetails = await (from fd in _airlineServiceContext.FlightBookingDetails 
                                             join ad in _airlineServiceContext.AirlineFlightDetails
                                             on fd.FlightId equals ad.Id
                                             join a in _airlineServiceContext.AirlineDetails
                                                  on ad.AirlineId equals a.AirlineId
                                             where fd.UserId == userId //&& fd.IsDelete == 0//&& fd.UserId == userId
                                             select new BookedTicketDetailsResponse()
                                             {
                                                 FlightNumber = ad.FlightNumber,
                                                 AirlineName = a.AirlineNmae, 
                                                 FromPlace = ad.FromPlaceName,
                                                 ToPlace = ad.ToPlaceName,
                                                 DepartureTime = ad.FlightStartDateTime,
                                                 ArrivalTime = ad.FlightToDateTime,
                                                 TotalCost = fd.TotalCosts,
                                                 Journey = fd.Journey == 0 ? "One Way" : "Two Way",
                                                 ClassStatus = fd.ClassStatus,
                                                 CancalStatus = ad.FlightStartDateTime <= DateTime.Now ? false : true,
                                                 PnrNumber = fd.PNR,
                                                 TotalSeats = fd.TotalBookSeats
                                             }

                                         ).ToListAsync();
                bookedTicketDetailsResponseList = new BookedTicketDetailsResponseList
                {

                    bookedTicketDetailsResponsesList = ticketBookedDetails
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("GetBookedTicketHistoryViaUserId Error : " + ex.Message.ToString());
            }
            return bookedTicketDetailsResponseList;
        }

        #endregion

        #region Private Method

        private static Random random = new Random();

        private static string GetRandomPNR()
        {
            int length = 12;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private double CalculateDiscount(double actualCost, int discountId)
        {
            #region define
            double totalCost = 0;

            #endregion
            try
            {
                var discountCost = _airlineServiceContext.Discounts.Where(d => d.DiscountId == discountId).Select(s => s.DiscountCost).FirstOrDefault();

                totalCost = actualCost - discountCost;
            }
            catch(Exception ex)
            {
                 totalCost = 0;
                _logger.LogError("CalculateDiscount Error : " + ex.Message.ToString());
            }
            return totalCost;
        }

        #endregion

    }
}
