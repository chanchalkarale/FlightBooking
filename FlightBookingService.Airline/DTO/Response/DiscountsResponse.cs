using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.DTO.Response
{
    public class DiscountsResponse
    {
        public string DiscountCode { get; set; }

        public int DiscountCost { get; set; }
        public int DiscountId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
