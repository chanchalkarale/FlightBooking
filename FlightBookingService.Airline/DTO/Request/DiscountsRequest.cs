using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Request
{
    public class DiscountsRequest
    {
        public string DiscountCode { get; set; }
        public int DiscountCost { get; set; }
    }
}
