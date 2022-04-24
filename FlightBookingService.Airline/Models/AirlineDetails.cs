using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.Airline.Models
{
    public class AirlineDetails
    {
        [Key]
        public int AirlineId { get; set; }
         
        public string AirlineNmae { get; set; }

        public int Status { get; set; } //0=Unblock , 1=Block

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        public int IsDelete { get; set; }
    }
}
