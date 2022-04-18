using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.User.Models
{
    public class UserRegistration
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string  EmailId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime InsertDate { get; set; }

        public int Flag { get; set; }

    }
}
