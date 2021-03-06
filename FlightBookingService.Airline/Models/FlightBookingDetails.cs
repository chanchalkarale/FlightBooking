using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Models
{
    public class FlightBookingDetails
    {
        [Key]
        public int FlightBookingId { get; set; }

        public int FlightId { get; set; } //pk of a AirlineFlightDetails

        public int UserId { get; set; }

        public int Journey { get; set; } // 1=One Way and 2= Round Trip

        public int ClassStatus { get; set; } //0=Business class ,1=Non Business class

        public decimal TotalCosts { get; set; } 

        public int TotalBookSeats { get; set; } 

        public string PNR { get; set; }

        public DateTime CreateDate { get; set; }
        public int IsDelete { get; set; }
        public int DiscountId { get; set; } //pk  of Discounts Table
    }
}
