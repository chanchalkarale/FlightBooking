using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Response
{
    public class GetAirlineResponse
    {
        public int AirlineId { get; set; }

        public string AirlineName { get; set; }

        public int Status { get; set; }
    }
}
