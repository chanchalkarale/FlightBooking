using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Response
{
    public class BookedTicketDetailsResponse
    {
        public string FlightNumber { get; set; }

        public string Airline { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int SeatNo { get; set; }

        public decimal TicketCost { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Journey { get; set; }

        public string Meal { get; set; }

        public int Age { get; set; }
    }
}
