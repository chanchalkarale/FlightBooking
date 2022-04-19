using FlightBookingService.Airline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Request
{
    public class UserBookingDetailsRequest
    {
      
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public GenderEnum Gender { get; set; } //0=Male ,1=Female

        public int Age { get; set; }
        public MealEnum Meal { get; set; } //0=None, 1=Veg, 2=Non-Veg

        public int SeatNumber { get; set; }

        public DateTime CreateDate { get; set; }
        public int Flag { get; set; }
    }
}
