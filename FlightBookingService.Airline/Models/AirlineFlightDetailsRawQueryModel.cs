using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Models
{
  
    public class AirlineFlightDetailsRawQueryModel
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }

        public string Airline { get; set; }

        public string FromPlaceName { get; set; }

        public string ToPlaceName { get; set; }

        public DateTime FlightStartDateTime { get; set; }
        public DateTime FlightToDateTime { get; set; }

        public int TotalBusinessSeats { get; set; }

        public int TotalNonBusinessSeats { get; set; }

        //public decimal BusTicketCost { get; set; }
        //public decimal NonBusTicketCost { get; set; }

        public decimal TicketCost { get; set; }

        public string ClassStatus { get; set; } //2=Business class ,1=Non Business class

        //public int FlightSeatRow { get; set; }

        //public int Meal { get; set; }
    }
}
