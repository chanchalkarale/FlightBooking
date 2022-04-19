﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Request
{
    public class FlightBookingDetailsRequest
    {
        public int FlightId { get; set; } //pk of a AirlineFlightDetails

        public int? FlightBookingId { get; set; } //pk of a FlightBookingDetails
        public int UserId { get; set; }

        public int Journey { get; set; } // 1=One Way and 2= Round Trip

        public double OneWayCost { get; set; }

        public double TwoWayCost { get; set; } 

        public int TotalBookSeats { get; set; }

        public string PNR { get; set; }

        public List<UserBookingDetailsRequest> userBookingDetailsRequestList { get; set; }

    }
}
