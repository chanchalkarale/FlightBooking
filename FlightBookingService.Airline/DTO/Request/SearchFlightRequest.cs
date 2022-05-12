using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Request
{
    public class SearchFlightRequest
    {
        public string JourneyType { get; set; } //0=One way 1=Two way 

        public string FromPlace { get; set; }
        public string ToPlace { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string seatsClass { get; set; } //1=Economy and 2=Business


    }
}
