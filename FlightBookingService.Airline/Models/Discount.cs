using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        public string DiscountCode { get; set; }
        public int DiscountCost { get; set; }
    }
}
