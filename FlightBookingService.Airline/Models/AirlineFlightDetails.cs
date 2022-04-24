using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Models
{
    public class AirlineFlightDetails
    {
        [Key]
        public int Id { get; set; }

        public string FlightNumber { get; set; }
         
        public int AirlineId { get; set; }

        [ForeignKey("AirlineId")]
        public virtual AirlineDetails Airlines { get; set; }

        public string FromPlaceName { get; set; }

        public string ToPlaceName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FlightStartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FlightToDateTime { get; set; }

        public int TotalBusinessSeats { get; set; }

        public int TotalNonBusinessSeats { get; set; }

        public decimal BusTicketCost { get; set; }

        public decimal NonBusTicketCost { get; set; }

        public int FlightSeatRow { get; set; }

        public MealEnum Meal { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; } 
        public int IsDelete { get; set; }
    }
}
